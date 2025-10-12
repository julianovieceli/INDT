using Dapper;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Infra.Mysql.Repository;
using Microsoft.Extensions.Logging;

namespace Insurance.INDT.Infra.Mysql.Repository
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

                string sql = "insert into Client(name, docto, age) values( @name, @docto, @age); ";

                var param = new 
                { 
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

        public async Task<Client> GetById(int id)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select * from Client where id = @id; ";

                var param = new { id };

                var client = await connection.QueryFirstOrDefaultAsync<Client>(sql, param); ;

                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Cliente");
                throw;
            }
        }
    }
}
