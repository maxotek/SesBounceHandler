using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SesBounceHandler.Models.Messaging;

namespace SesBounceHandler
{
    public class EmailLogProcessor : IDisposable
    {
        private readonly ILogger<EmailLogProcessor> _logger;

        // Constants
        private const string QueueName = "email-log-queue";

        private const int BatchSize = 10;
        private const int ReceiveWaitTimeSeconds = 30;

        private const int EmptyQueueSleepMilliseconds = 1000 * 60 * 15; // 15 min

        // State
        private string _queueUrl;

        // Services
        private readonly AmazonSQSClient _sqsClient;

        public EmailLogProcessor(ILogger<EmailLogProcessor> logger)
        {
            _logger = logger;
            _sqsClient = new AmazonSQSClient();
        }

        public void Dispose()
        {
            _sqsClient.Dispose();
        }

        public async Task StartPolling(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Email log processor");

            var getQueueUrlResponse = await _sqsClient.GetQueueUrlAsync(QueueName, cancellationToken);
            _queueUrl = getQueueUrlResponse.QueueUrl;

            while (!cancellationToken.IsCancellationRequested)
            {
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = _queueUrl,
                    MaxNumberOfMessages = BatchSize,
                    WaitTimeSeconds = ReceiveWaitTimeSeconds
                };

                var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(request, cancellationToken);
                if (receiveMessageResponse.Messages.Count == 0)
                {
                    await Task.Delay(EmptyQueueSleepMilliseconds, cancellationToken);
                    continue;
                }

                await ProcessMessages(receiveMessageResponse.Messages, cancellationToken);
            }
        }

        private async Task ProcessMessages(List<Message> messages, CancellationToken cancellationToken)
        {
            var deleteMessageBatchRequestEntries = new List<DeleteMessageBatchRequestEntry>();
            var bouncedEmailAddresses = new HashSet<string>();

            foreach (var message in messages)
            {
                var emailEvent = JsonConvert.DeserializeObject<SesEmailEvent>(message.Body);
                
                var bouncedEmailAddress = await ProcessEvent(emailEvent);
                if (bouncedEmailAddress != null)
                    bouncedEmailAddresses.Add(bouncedEmailAddress);

                var deleteMessageBatchRequestEntry = new DeleteMessageBatchRequestEntry(message.MessageId, message.ReceiptHandle);
                deleteMessageBatchRequestEntries.Add(deleteMessageBatchRequestEntry);
            }

            await ConsumeMessages(deleteMessageBatchRequestEntries, cancellationToken);
            // do something with bounced email addresses
        }

        private static async Task<string> ProcessEvent(SesEmailEvent emailEvent)
        {
            var eventType = emailEvent.EventType;
            var bounce = emailEvent.Bounce;

            if (eventType == "Bounce" && bounce!.BounceType == "Permanent")
            {
                var mailAddress = new MailAddress(bounce.BouncedRecipients.First().EmailAddress);
                return mailAddress.Address;
            }

            return null;
        }

        private async Task ConsumeMessages(List<DeleteMessageBatchRequestEntry> entries, CancellationToken cancellationToken)
        {
            if (entries.Count <= 0)
                return;

            var deleteMessageBatchRequest = new DeleteMessageBatchRequest
            {
                QueueUrl = _queueUrl,
                Entries = entries
            };

            await _sqsClient.DeleteMessageBatchAsync(deleteMessageBatchRequest, cancellationToken);
            
            // Remove Batch
            entries.Clear();
        }
    }
}