using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;
using System.Text;

namespace Insurance.INDT.Application.ServiceBus
{
    public class ServiceBusClientService : IServiceBusClientService
    {
        private readonly IAzureClientFactory<ServiceBusSender> _azureClientFactory;

        private readonly ServiceBusSender _serviceBusSender;
        public ServiceBusClientService(IAzureClientFactory<ServiceBusSender> azureClientFactory)
        {
            _azureClientFactory = azureClientFactory;
            _serviceBusSender = _azureClientFactory.CreateClient("TesteMensagem");
        }


        public async Task SendMessage(string msg)
        {
            ServiceBusMessage sbMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(msg));

            sbMessage.To = "subscription.1";
            sbMessage.Body = new BinaryData(msg);
            sbMessage.ContentType = "application/json";

            await _serviceBusSender.SendMessageAsync(sbMessage);




        }
    }
}
