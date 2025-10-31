using INDT.Common.Insurance.Dto;
using INDT.Common.Insurance.Infra.Interfaces.AWS;

namespace Insurance.INDT.Worker
{
    public class AwsLambdaFunctionClientServiceWorker: BackgroundService
    {

        private readonly ILogger<AwsLambdaFunctionClientServiceWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public AwsLambdaFunctionClientServiceWorker(ILogger<AwsLambdaFunctionClientServiceWorker> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Create a periodic timer that triggers every 10 seconds
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Executing scheduled task at: {time}", DateTime.UtcNow);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var consumerService = scope.ServiceProvider.GetRequiredService<IAWSLambdaFunctionClientService>();
                    await consumerService.InvokeLambdaAsync(new RequestLambdaTestDto
                    {
                        Nome = "Nome-" + Guid.NewGuid().ToString(),
                    });
                }
            }
        }
    }
}
