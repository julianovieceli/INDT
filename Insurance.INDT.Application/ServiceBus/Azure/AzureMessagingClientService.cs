using Azure.Messaging.ServiceBus;
using INDT.Common.Insurance.Domain.Interfaces.Infra;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Insurance.INDT.Application.ServiceBus.Azure
{
    public class AzureMessagingClientService : IAzureMessagingClientService
    {
        private readonly IAzureClientFactory<ServiceBusSender> _azureClientFactory;

        private readonly ServiceBusSender _serviceBusSender;

        private readonly ILogger<AzureMessagingClientService> _logger;
        public AzureMessagingClientService(IAzureClientFactory<ServiceBusSender> azureClientFactory, ILogger<AzureMessagingClientService> logger)
        {
            _azureClientFactory = azureClientFactory;
            _serviceBusSender = _azureClientFactory.CreateClient("TesteMensagem");
            _logger = logger;
        }


        public async Task SendMessage<T>(T msg)
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

            services.AddScoped<IAzureMessagingClientService, AzureMessagingClientService>();


            return services;
        }
    }


}
