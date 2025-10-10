using Insurance.INDT.Domain;
using Insurance.INDT.Dto.Request;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IInsuranceService
    {
        Task<Result> Register(RegisterInsuranceDto resisterInsuranceDto);

        Task<Result> GetByName(string docto);


        Task<Result> GetAll();
    }
}
