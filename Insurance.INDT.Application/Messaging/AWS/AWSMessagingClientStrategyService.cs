using Amazon.SQS;
using Amazon.SQS.Model;
using INDT.Common.Insurance.Infra.Interfaces.AWS;
using Insurance.INDT.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Insurance.INDT.Application.Messaging.AWS
{
    public class AWSMessagingClientStrategyService: IAWSMessagingClientStrategyService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger<AWSMessagingClientStrategyService> _logger;
        private const string QueueName = "QueueTest"; // Replace with your queue name

        public AWSMessagingClientStrategyService(IAmazonSQS sqsClient, ILogger<AWSMessagingClientStrategyService> logger)
        {
            _sqsClient = sqsClient;
            _logger = logger;
        }

        public async Task SendMessage<T>(T msg)
        {
            try
            {
                // Get queue URL (or create if it doesn't exist on LocalStack)
                var getQueueUrlResponse = await _sqsClient.GetQueueUrlAsync(new GetQueueUrlRequest { QueueName = QueueName });
                var queueUrl = getQueueUrlResponse.QueueUrl;

                string messageBody =  JsonSerializer.Serialize(msg);
                // Send a message
                var sendMessageRequest = new SendMessageRequest
                {
                    QueueUrl = queueUrl,
                    MessageBody = messageBody,
                    // Optional: Add message attributes
                    // MessageAttributes = new Dictionary<string, MessageAttributeValue>
                    // {
                    //     { "MessageType", new MessageAttributeValue { StringValue = "Test", DataType = "String" } }
                    // }
                };

                var sendMessageResponse = await _sqsClient.SendMessageAsync(sendMessageRequest);
                
                _logger.LogInformation($"Message sent to AWS successfully to queue '{QueueName}'. Message ID: {sendMessageResponse.MessageId}");
                

            }
            catch (QueueDoesNotExistException ex)
            {
                _logger.LogError($"Error: Queue '{QueueName}' does not exist. Please create it in LocalStack. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }

        }
    }

    public static class IoC
    {
        public static IServiceCollection AddAWSMessagingClientService(this IServiceCollection services, IConfiguration configuration)
        {
            AmazonSqSConfig amazonSQSConfig = new AmazonSqSConfig();
            configuration.GetSection("AmazonSQSConfig").Bind(amazonSQSConfig);

            services.AddSingleton<IAmazonSQS>(sp =>
            {
                var config = new AmazonSQSConfig
                {
                    ServiceURL = amazonSQSConfig.ServiceURL,
                    AuthenticationRegion = amazonSQSConfig.AuthenticationRegion
                };
                return new AmazonSQSClient(amazonSQSConfig.awsAccessKeyId, amazonSQSConfig.awsSecretAccessKey, config); // Dummy credentials
            });


            services.AddScoped<IAWSMessagingClientStrategyService, AWSMessagingClientStrategyService>();
            return services;
        }
    }
}
