namespace INDT.Common.Insurance.Infra.Interfaces
{
    public interface IMessagingClientService
    {
        Task SendMessage<T>(T msg);
    }
}
