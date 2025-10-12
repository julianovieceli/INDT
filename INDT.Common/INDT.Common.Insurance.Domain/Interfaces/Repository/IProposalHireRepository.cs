using ProposalHireDomain = INDT.Common.Insurance.Domain.ProposalHire;

namespace INDT.Common.Insurance.Domain.Interfaces.Repository
{
    public interface IProposalHireRepository
    {

        Task<List<ProposalHire>> GetAll();

        Task<bool> Register(ProposalHireDomain proposalHire);

        Task<int> GetCounByProposalId(int proposalId);

    }
}
