using Insurance.INDT.Dto.Request;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<Result> Register(RegisterClientDto registerClient);

        Task<Result> GetByDocto(string docto);


        Task<Result> GetAll();
    }
}
