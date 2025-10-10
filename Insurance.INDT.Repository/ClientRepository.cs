using Dapper;
using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Interfaces.Repository;

namespace Insurance.INDT.Repository
{
    public class ClientRepository: IClientRepository
    {
        private readonly IDbContext _dbContext;

        public ClientRepository(IDbContext dbContext)
        {
                _dbContext = dbContext;
        }

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

        public async Task<List<Client>> GetAll()
        {
            try
            {
                var connection = _dbContext.Connect();
                
                string sql = "select * from Client; ";
                
                var result = await connection.QueryAsync<Client>(sql);;
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
