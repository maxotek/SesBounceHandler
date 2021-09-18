using System.IO;
using System.Threading.Tasks;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MimeKit;

namespace SesEmailSender
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var sesClient = new AmazonSimpleEmailServiceClient();
            
            var message = new MimeMessage
            {
                Headers = { new Header("X-SES-CONFIGURATION-SET", "app-config-set") },
                From = { new MailboxAddress("App", "app@spacez.com") },
                To = { new MailboxAddress("Elon Musk", "elon.musk@spacez.com") },
                Subject = "Test Email",
                Body = new BodyBuilder { TextBody = "This is a test message" }.ToMessageBody()
            };

            await using var messageStream = new MemoryStream();
            await message.WriteToAsync(messageStream);

            var request = new SendRawEmailRequest(new RawMessage { Data = messageStream });
            await sesClient.SendRawEmailAsync(request);
        }
    }
}