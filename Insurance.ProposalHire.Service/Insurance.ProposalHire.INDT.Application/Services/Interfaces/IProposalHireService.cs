using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Request;

namespace Insurance.ProposalHire.INDT.Application.Services.Interfaces
{
    public interface IProposalHireService
    {
        Task<Result> Register(HireProposalDto hireProposalDto);
    }
}
