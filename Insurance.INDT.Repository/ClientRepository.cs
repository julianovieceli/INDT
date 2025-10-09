using Insurance.INDT.Domain.Interfaces.Repository;

namespace Insurance.INDT.Repository
{
    public class ClientRepository: IClientRepository
    {
        public Task<bool> GetByDocto(string docto)
        {
            return Task.FromResult(true);
        }
    }
}
