namespace Insurance.INDT.Worker
{
    public class AwsLambdaFunctionClientServiceWorker: BackgroundService
    {

        private readonly ILogger<AwsLambdaFunctionClientServiceWorker> _logger;
        public AwsLambdaFunctionClientServiceWorker(ILogger<AwsLambdaFunctionClientServiceWorker> logger)
        {
            _logger = logger;
     
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Create a periodic timer that triggers every 10 seconds
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Executing scheduled task at: {time}", DateTime.UtcNow);
                // Add your scheduled work here
            }
        }
    }
}
