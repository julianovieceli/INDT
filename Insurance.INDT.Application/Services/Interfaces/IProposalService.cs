using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Request;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IProposalService
    {
        Task<Result> Register(RegisterProposalDto proposalDto);

        Task<Result> GetById(int id);


        Task<Result> GetAll();

        Task<Result> UpdateStatus(UpdateProposalDto updateProposalDto);
    }
}
