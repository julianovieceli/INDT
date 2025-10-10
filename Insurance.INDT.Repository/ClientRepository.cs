using Dapper;
using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Mysql.Repository;
using Microsoft.Extensions.Logging;

namespace Insurance.INDT.Repository
{
    public class ClientRepository: IClientRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<MySqlDbcontext> _logger;

        public ClientRepository(IDbContext dbContext, ILogger<MySqlDbcontext> logger)
        {
                _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Client> GetByDocto(string docto)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select * from Client where docto = @docto; ";

                var param = new { docto };

                var client = await connection.QueryFirstOrDefaultAsync<Client>(sql, param); ;

                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Cliente");
                throw;
            }
        }
        public async Task<int> GetCountByDocto(string docto)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select count(*) from Client where docto = @docto; ";

                var param = new { docto };

                var total = await connection.ExecuteScalarAsync<int>(sql, param); ;

                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Cliente");
                throw;
            }
        }

        public async Task<bool> Register(Client client)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "insert into Client(id, name, docto, age) values(@id, @name, @docto, @age); ";

                var param = new 
                { 
                    id = client.Id, 
                    name = client.Name,
                    docto = client.Docto,
                    age = client.Age
                };

                var result = await connection.ExecuteAsync(sql, param);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar um Cliente");
                throw;
            }
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
                _logger.LogError(ex, "Erro ao consultar clientes");
                return null;
            }

        }
    }
}
