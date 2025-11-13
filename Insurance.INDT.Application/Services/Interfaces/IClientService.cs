using INDT.Common.Insurance.Dto.Request;
using Personal.Common.Domain;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<Result> Register(RegisterClientDto registerClient);

        Task<Result> GetByDocto(string docto);


        Task<Result> GetAll();
    }
}
