using INDT.Common.Insurance.Domain.Enums;

namespace INDT.Common.Insurance.Domain.Interfaces.Repository
{
    public interface IProposalRepository
    {
        Task<Proposal> GetByClientIdAndInsuranceId(int clientId, int insuranceId);

        Task<Proposal> GetById(int id);

        Task<List<Proposal>> GetAll();

        Task<bool> Register(Proposal proposal);

        Task<bool> UpdateStatus(int proposalId, ProposalStatus statusId);

    }
}
