using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Infra.Mysql.Repository;
using Microsoft.Extensions.Logging;
using ProposalHireDomain = INDT.Common.Insurance.Domain.ProposalHire;
using InsuranceDomain = INDT.Common.Insurance.Domain;
using Dapper;

namespace Insurance.ProposalHire.INDT.MySql.Repository
{
    public class ProposalHireRepository : IProposalHireRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<MySqlDbcontext> _logger;

        public ProposalHireRepository(IDbContext dbContext, ILogger<MySqlDbcontext> logger)
        {
                _dbContext = dbContext;
            _logger = logger;
        }


        //public async Task<bool> Register(ProposalHire proposal)
        //{
        //    try
        //    {
        //        var connection = _dbContext.Connect();

        //        string sql = "insert into Proposal(clientId, insuranceId, value, statusId, expirationDate, creationDate) " +
        //            "values( @clientId, @insuranceId, @value, @statusId, @expirationDate, @creationDate); ";

        //        var param = new
        //        {
        //            clientId = proposal.Client.Id,
        //            insuranceId = proposal.Insurance.Id,
        //            creationDate = proposal.CreationDate,
        //            value = proposal.Value,
        //            expirationDate = proposal.ExpirationDate,
        //            statusId = proposal.StatusId
        //        };

        //        var result = await connection.ExecuteAsync(sql, param);

        //        return result > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao criar uma proposta");
        //        throw;
        //    }
        //}




        public async Task<List<ProposalHireDomain>> GetAll()
        {
            try
            {
                var connection = _dbContext.Connect();

                string sql = "select ph.id, ph.description,  ph.creationDate, ph.expirationDate, p.statusId, p.value, c.name, c.age, " +
                    "c.docto, p.insuranceId, i.id, i.name  from Proposal p join Client c on (p.clientId = c.id)" +
                    " join Insurance i on (p.insuranceId = i.id) " +
                    "join ProposalHire ph on (ph.proposalId = p.id)";


                var proposal = await connection.QueryAsync<ProposalHireDomain, Proposal, Client, InsuranceDomain.Insurance, ProposalHireDomain>
                    (sql, map: (propHire, prop, cl, ins) => {
                        propHire.Proposal = prop;
                        prop.Client = cl;
                        prop.Insurance = ins;
                        return propHire;
                    },  splitOn: "proposalId, clientId, insuranceId");

                return proposal.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma proposta");
                throw;
            }

        }


    }
}
