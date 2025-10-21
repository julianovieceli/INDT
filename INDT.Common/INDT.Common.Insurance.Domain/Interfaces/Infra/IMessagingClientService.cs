namespace INDT.Common.Insurance.Domain.Interfaces.Infra
{
    public interface IMessagingClientService
    {
        Task SendMessage<T>(T msg);
    }
}
