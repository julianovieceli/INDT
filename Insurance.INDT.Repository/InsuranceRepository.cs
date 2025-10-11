using Dapper;
using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Mysql.Repository;
using Microsoft.Extensions.Logging;

namespace Insurance.INDT.Repository
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<MySqlDbcontext> _logger;

        public InsuranceRepository(IDbContext dbContext, ILogger<MySqlDbcontext> logger)
        {
                _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Domain.Insurance> GetByName(string name)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select * from Insurance where name = @name; ";

                var param = new {name };

                var insurance = await connection.QueryFirstOrDefaultAsync<Domain.Insurance>(sql, param); ;

                return insurance;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Seguro");
                throw;
            }
        }

        public async Task<Domain.Insurance> GetById(int id)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select * from Insurance where id = @id; ";

                var param = new { id };

                var insurance = await connection.QueryFirstOrDefaultAsync<Domain.Insurance>(sql, param); ;

                return insurance;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Seguro");
                throw;
            }
        }

        public async Task<int> GetCountByName(string name)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select count(*) from Insurance where name = @name; ";

                var param = new { name };

                var total = await connection.ExecuteScalarAsync<int>(sql, param); ;

                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Seguro");
                throw;
            }
        }

        public async Task<bool> Register(Domain.Insurance insurance)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "insert into Insurance(name, creationDate) values( @name, @creationDate); ";

                var param = new 
                { 
                    name = insurance.Name,
                    creationDate = insurance.CreationDate
                };

                var result = await connection.ExecuteAsync(sql, param);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar um Seguro");
                throw;
            }
        }

        public async Task<List<Domain.Insurance>> GetAll()
        {
            try
            {
                var connection = _dbContext.Connect();
                
                string sql = "select * from Insurance; ";
                
                var result = await connection.QueryAsync<Domain.Insurance>(sql);;
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar seguros");
                return null;
            }

        }
    }
}
