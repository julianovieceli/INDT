using Dapper;
using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Mysql.Repository;
using Microsoft.Extensions.Logging;

namespace Insurance.INDT.Repository
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<MySqlDbcontext> _logger;

        public ProposalRepository(IDbContext dbContext, ILogger<MySqlDbcontext> logger)
        {
                _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Domain.Proposal> GetById(int id)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select p.*, c.name as clientName, i.name as insuranceName from Proposal p join Client c on (p.clientId = c.id)" +
                    " join Insurance i on (p.insuranceId = i.id) where p.id = @id; ";

                var param = new {id };

                var proposal = await connection.QueryAsync<Proposal, Client, Domain.Insurance, Proposal>
                    (sql, map: (prop, cl, ins) => {
                        prop.client = cl;
                        prop.Insurance = ins;
                        return prop;
                    }, param:param, splitOn: "clientId, insuranceId");

                return proposal.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma proposta");
                throw;
            }
        }
        //public async Task<int> GetCountByName(string name)
        //{
        //    try
        //    {
        //        var connection = _dbContext.Connect();

        //        string sql = "select count(*) from Insurance where name = @name; ";

        //        var param = new { name };

        //        var total = await connection.ExecuteScalarAsync<int>(sql, param); ;

        //        return total;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao consultar um Seguro");
        //        throw;
        //    }
        //}

        //public async Task<bool> Register(Domain.Insurance insurance)
        //{
        //    try
        //    {
        //        var connection = _dbContext.Connect();

        //        string sql = "insert into Insurance(name, creationDate) values( @name, @creationDate); ";

        //        var param = new 
        //        { 
        //            name = insurance.Name,
        //            creationDate = insurance.CreationDate
        //        };

        //        var result = await connection.ExecuteAsync(sql, param);

        //        return result > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao cadastrar um Seguro");
        //        throw;
        //    }
        //}

        //public async Task<List<Domain.Insurance>> GetAll()
        //{
        //    try
        //    {
        //        var connection = _dbContext.Connect();
                
        //        string sql = "select * from Insurance; ";
                
        //        var result = await connection.QueryAsync<Domain.Insurance>(sql);;
        //        return result.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao consultar seguros");
        //        return null;
        //    }

        //}
    }
}
