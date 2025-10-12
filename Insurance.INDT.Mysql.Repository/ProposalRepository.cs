using Dapper;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Enums;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Infra.Mysql.Repository;
using Microsoft.Extensions.Logging;
using InsuranceDomain = INDT.Common.Insurance.Domain;


namespace Insurance.INDT.Infra.Mysql.Repository
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

        public async Task<Proposal> GetByClientIdAndInsuranceId(int clientId, int insuranceId)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select p.id, p.value, p.statusId, p.expirationDate, p.clientId, c.id, c.name, c.age, c.docto, p.insuranceId, i.id, i.name  from Proposal p join Client c on (p.clientId = c.id)" +
                    " join Insurance i on (p.insuranceId = i.id) where p.clientId = @clientId and p.insuranceId = @insuranceId; ";

                var param = new { clientId , insuranceId };

                var proposal = await connection.QueryAsync<Proposal, Client, InsuranceDomain.Insurance, Proposal>
                    (sql, map: (prop, cl, ins) => {
                        prop.Client = cl;
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

        public async Task<Proposal> GetById(int id)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select p.id, p.value, p.statusId, p.creationDate, p.expirationDate, p.clientId, c.id, c.name, c.age, c.docto, p.insuranceId, i.id, i.name  from Proposal p join Client c on (p.clientId = c.id)" +
                 " join Insurance i on (p.insuranceId = i.id) where p.id = @id";


                var param = new { id };

                var proposal = await connection.QueryAsync<Proposal, Client, InsuranceDomain.Insurance, Proposal>
                    (sql, map: (prop, cl, ins) => {
                        prop.Client = cl;
                        prop.Insurance = ins;
                        return prop;
                    }, param: param, splitOn: "clientId, insuranceId");

                return proposal.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma proposta");
                throw;
            }
        }

        public async Task<bool> Register(Proposal proposal)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "insert into Proposal(clientId, insuranceId, value, statusId, expirationDate, creationDate) " +
                    "values( @clientId, @insuranceId, @value, @statusId, @expirationDate, @creationDate); ";

                var param = new
                {
                    clientId = proposal.Client.Id,
                    insuranceId = proposal.Insurance.Id,
                    creationDate = proposal.CreationDate,
                    value = proposal.Value,
                    expirationDate = proposal.ExpirationDate,
                    statusId = proposal.StatusId
                };

                var result = await connection.ExecuteAsync(sql, param);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar uma proposta");
                throw;
            }
        }


        public async Task<List<Proposal>> GetAll()
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select p.id, p.value, p.statusId, p.creationDate, p.expirationDate, p.clientId, c.id, c.name, c.age, c.docto, p.insuranceId, i.id, i.name  from Proposal p join Client c on (p.clientId = c.id)" +
                    " join Insurance i on (p.insuranceId = i.id)";


                var proposal = await connection.QueryAsync<Proposal, Client, InsuranceDomain.Insurance, Proposal>
                    (sql, map: (prop, cl, ins) => {
                        prop.Client = cl;
                        prop.Insurance = ins;
                        return prop;
                    },  splitOn: "clientId, insuranceId");

                return proposal.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma proposta");
                throw;
            }

        }


        public async Task<bool> UpdateStatus(int proposalId, ProposalStatus statusId)
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "update Proposal set statusId = @statusId where id = @id";

                var param = new
                {
                    id = proposalId,
                    statusId
                };

                var result = await connection.ExecuteAsync(sql, param);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o status de  uma proposta");
                throw;
            }
        }
    }
}
