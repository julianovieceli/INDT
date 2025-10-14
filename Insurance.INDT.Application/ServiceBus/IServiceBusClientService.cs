namespace Insurance.INDT.Application.ServiceBus
{
    public interface IServiceBusClientService
    {
        Task SendMessage(string msg);
    }
}
