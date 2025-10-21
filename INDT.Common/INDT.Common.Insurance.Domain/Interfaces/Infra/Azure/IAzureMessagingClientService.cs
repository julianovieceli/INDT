namespace INDT.Common.Insurance.Domain.Interfaces.Infra
{
    public interface IAzureMessagingClientService : IMessagingClientService
    {
        new Task SendMessage<T>(T msg);
    }
}
