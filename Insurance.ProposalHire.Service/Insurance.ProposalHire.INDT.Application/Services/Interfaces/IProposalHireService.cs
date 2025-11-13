using INDT.Common.Insurance.Dto.Request;
using Personal.Common.Domain;

namespace Insurance.ProposalHire.INDT.Application.Services.Interfaces
{
    public interface IProposalHireService
    {
        Task<Result> Register(HireProposalDto hireProposalDto);

        Task<Result> GetAll();
    }
}
