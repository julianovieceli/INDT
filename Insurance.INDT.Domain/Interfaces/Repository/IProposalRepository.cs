using Insurance.INDT.Domain.Enums;

namespace Insurance.INDT.Domain.Interfaces.Repository
{
    public interface IProposalRepository
    {
        Task<Domain.Proposal> GetByClientIdAndInsuranceId(int clientId, int insuranceId);

        Task<Domain.Proposal> GetById(int id);

        Task<List<Domain.Proposal>> GetAll();

        Task<bool> Register(Proposal proposal);

        Task<bool> UpdateStatus(int proposalId, ProposalStatus statusId);

    }
}
