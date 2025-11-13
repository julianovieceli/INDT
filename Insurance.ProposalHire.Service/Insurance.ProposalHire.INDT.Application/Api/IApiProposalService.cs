using INDT.Common.Insurance.Domain;
using Personal.Common.Domain;

namespace Insurance.ProposalHire.INDT.Application.Api
{
    public interface IApiProposalService
    {

        Task<Result> GetProposalById(int id);
    }
}
