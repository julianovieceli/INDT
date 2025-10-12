using INDT.Common.Insurance.Domain;

namespace Insurance.ProposalHire.INDT.Application.Services.Interfaces
{
    public interface IProposalHireService
    {
        Task<Result> Register(int proposalId);
    }
}
