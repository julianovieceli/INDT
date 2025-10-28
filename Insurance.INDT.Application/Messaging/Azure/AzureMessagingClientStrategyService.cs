using Amazon.SQS.Model;
using Azure.Messaging.ServiceBus;
using INDT.Common.Insurance.Infra.Interfaces.Azure;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Insurance.INDT.Application.Messaging.Azure
{
    public class AzureMessagingClientStrategyService : IAzureMessagingClientStrategyService
    {
        private readonly IAzureClientFactory<ServiceBusSender> _azureClientFactory;

        private readonly ServiceBusSender _serviceBusSender;

        private readonly ILogger<AzureMessagingClientStrategyService> _logger;
        public AzureMessagingClientStrategyService(IAzureClientFactory<ServiceBusSender> azureClientFactory, ILogger<AzureMessagingClientStrategyService> logger)
        {
            _azureClientFactory = azureClientFactory;
            _serviceBusSender = _azureClientFactory.CreateClient("TesteMensagem");
            _logger = logger;
        }


        public async Task SendMessage<T>(T msg)
        {
            try
            {
                string messageBody = JsonSerializer.Serialize(msg);

                ServiceBusMessage sbMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

                sbMessage.To = "subscription.1";
                sbMessage.Body = new BinaryData(messageBody);
                sbMessage.ContentType = "application/json";

                _logger.LogInformation($"Enviando msg :{messageBody}");


                _logger.LogInformation($"EntityPath  :{_serviceBusSender.EntityPath.ToString()}");
                _logger.LogInformation($"FullyQualifiedNamespace:{_serviceBusSender.FullyQualifiedNamespace}");

                await _serviceBusSender.SendMessageAsync(sbMessage);

                _logger.LogInformation($"Message sent to Azure successfully to queue '{sbMessage.To}'. Message ID: {sbMessage.MessageId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }



        }

        

    }

    public static class Ioc
    {
        public static IServiceCollection AddAzureMessagingClientService(this IServiceCollection services, IConfiguration configuration)
        {
            var connStringSection = configuration.GetSection("ServiceBus:ServiceBusConnection");

            var topic = configuration.GetSection("ServiceBus:TopicName");

            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(connStringSection.Value);


                builder.AddClient<ServiceBusSender, ServiceBusSenderOptions>((options, provider) =>
                {
                    var serviceBusClient = provider.GetRequiredService<ServiceBusClient>();
                    return serviceBusClient.CreateSender(topic.Value);
                }).WithName("TesteMensagem");
            });

            services.AddScoped<IAzureMessagingClientStrategyService, AzureMessagingClientStrategyService>();


            return services;
        }
    }


}
