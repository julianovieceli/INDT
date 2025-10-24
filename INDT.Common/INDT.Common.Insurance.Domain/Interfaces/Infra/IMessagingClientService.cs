namespace INDT.Common.Insurance.Domain.Interfaces.Infra
{
    public interface IMessagingClientStrategyService
    {
        Task SendMessage<T>(T msg);
    }
}
