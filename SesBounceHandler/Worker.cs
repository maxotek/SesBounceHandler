using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SesBounceHandler
{
    public class Worker : BackgroundService
    {
        private readonly EmailLogProcessor _emailLogProcessor;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, EmailLogProcessor emailLogProcessor)
        {
            _logger = logger;
            _emailLogProcessor = emailLogProcessor;
        }

        public override void Dispose()
        {
            base.Dispose();
            _emailLogProcessor.Dispose();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting worker");
            await _emailLogProcessor.StartPolling(stoppingToken);
        }
    }
}