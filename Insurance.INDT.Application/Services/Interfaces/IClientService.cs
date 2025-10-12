using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Request;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<Result> Register(RegisterClientDto registerClient);

        Task<Result> GetByDocto(string docto);


        Task<Result> GetAll();
    }
}
