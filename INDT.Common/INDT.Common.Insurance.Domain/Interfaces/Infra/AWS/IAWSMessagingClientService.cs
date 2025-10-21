namespace INDT.Common.Insurance.Domain.Interfaces.Infra
{
    public interface IAWSMessagingClientService: IMessagingClientService
    {
        new Task SendMessage<T>(T msg);
    }
}
