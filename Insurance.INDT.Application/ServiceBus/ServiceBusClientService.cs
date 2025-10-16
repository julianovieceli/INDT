using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Insurance.INDT.Application.ServiceBus
{
    public class ServiceBusClientService : IServiceBusClientService
    {
        private readonly IAzureClientFactory<ServiceBusSender> _azureClientFactory;

        private readonly ServiceBusSender _serviceBusSender;

        private readonly ILogger<ServiceBusClientService> _logger;
        public ServiceBusClientService(IAzureClientFactory<ServiceBusSender> azureClientFactory, ILogger<ServiceBusClientService> logger)
        {
            _azureClientFactory = azureClientFactory;
            _serviceBusSender = _azureClientFactory.CreateClient("TesteMensagem");
            _logger = logger;
        }


        public async Task SendMessage(string msg)
        {
            ServiceBusMessage sbMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(msg));

            sbMessage.To = "subscription.1";
            sbMessage.Body = new BinaryData(msg);
            sbMessage.ContentType = "application/json";

            _logger.LogInformation($"Enviando msg :{msg}");


            _logger.LogInformation($"EntityPath  :{_serviceBusSender.EntityPath.ToString()}");
            _logger.LogInformation($"FullyQualifiedNamespace:{_serviceBusSender.FullyQualifiedNamespace}");
            
            await _serviceBusSender.SendMessageAsync(sbMessage);




        }
    }
}
