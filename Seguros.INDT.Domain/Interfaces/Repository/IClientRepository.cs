namespace Insurance.INDT.Domain.Interfaces.Repository
{
    public interface IClientRepository
    {
        Task<bool> GetByDocto(string docto);
    }
}
