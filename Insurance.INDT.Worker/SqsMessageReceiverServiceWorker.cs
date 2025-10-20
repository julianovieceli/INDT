namespace Insurance.INDT.Worker
{
    using Amazon.SQS;
    using Amazon.SQS.Model;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class SqsMessageReceiverServiceWorker : BackgroundService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger<SqsMessageReceiverServiceWorker> _logger;
        private const string QueueName = "QueueTest"; // Replace with your queue name

        public SqsMessageReceiverServiceWorker(IAmazonSQS sqsClient, ILogger<SqsMessageReceiverServiceWorker> logger)
        {
            _sqsClient = sqsClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SQS Message Receiver Service started.");

            try
            {
                // Get queue URL (or create if it doesn't exist on LocalStack)
                var getQueueUrlResponse = await _sqsClient.GetQueueUrlAsync(new GetQueueUrlRequest { QueueName = QueueName });
                var queueUrl = getQueueUrlResponse.QueueUrl;

                while (!stoppingToken.IsCancellationRequested)
                {
                    var receiveMessageRequest = new ReceiveMessageRequest
                    {
                        QueueUrl = queueUrl,
                        MaxNumberOfMessages = 1,
                        WaitTimeSeconds = 5 // Long polling
                    };

                    var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);

                    if (receiveMessageResponse.Messages is not null)
                    {

                        foreach (var message in receiveMessageResponse.Messages)
                        {
                            _logger.LogInformation($"Received message: {message.Body}");
                            // Process your message here

                            // Delete the message after processing
                            await _sqsClient.DeleteMessageAsync(new DeleteMessageRequest
                            {
                                QueueUrl = queueUrl,
                                ReceiptHandle = message.ReceiptHandle
                            }, stoppingToken);
                        }
                    }
                    else
                        _logger.LogInformation("No SQS Message Received.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SQS Message Receiver Service.");
            }

            _logger.LogInformation("SQS Message Receiver Service stopped.");
        }
    }
}
