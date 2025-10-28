namespace Insurance.INDT.Worker
{
    using global::INDT.Common.Insurance.Infra.Interfaces.Rabbit;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class RabbitMqMessageReceiverServiceWorker : BackgroundService, IDisposable
    {
        private readonly ILogger<RabbitMqMessageReceiverServiceWorker> _logger;
        private const string QueueName = "QueueTest"; // Replace with your queue name
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMqMessageReceiverServiceWorker(IServiceScopeFactory scopeFactory, ILogger<RabbitMqMessageReceiverServiceWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RabbitMq Background  Message Receiver Service started.");


            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var consumerService = scope.ServiceProvider.GetRequiredService<IRabbitMqMessagingClientStrategyService>();
                        consumerService.StartConsuming();
                    }
                    // You might want to add a delay here or rely on the consumer's event-driven nature
                    // For a continuous consumer, the StartConsuming method will typically establish the listener
                    // and the loop might not be strictly necessary if the consumer handles its own lifecycle.
                    // However, for certain scenarios, you might want to re-establish the consumer periodically.
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Example delay
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RabbitMQ Message Receiver Service.");
            }

            _logger.LogInformation("RabbitMQ Background Consumer Service is stopping.");


        }


    }
}
