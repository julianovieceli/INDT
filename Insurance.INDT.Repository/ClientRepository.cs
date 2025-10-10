using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Interfaces.Repository;

namespace Insurance.INDT.Repository
{
    public class ClientRepository: IClientRepository
    {
        public Task<Client> GetByDocto(string docto)
        {
            try
            {
                return Task.FromResult(new Client());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<bool> Register(Client client)
        {
            return Task.FromResult(true);
        }

        public Task<List<Client>> GetAll()
        {
            return Task.FromResult(new List<Client>());
        }
    }
}
