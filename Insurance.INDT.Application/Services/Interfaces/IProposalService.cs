using Insurance.INDT.Domain;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IProposalService
    {
        //Task<Result> Register(RegisterInsuranceDto resisterInsuranceDto);

        Task<Result> GetById(int id);


        //Task<Result> GetAll();
    }
}
