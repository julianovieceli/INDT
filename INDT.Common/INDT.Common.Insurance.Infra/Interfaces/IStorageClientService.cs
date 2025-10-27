using INDT.Common.Insurance.Domain;

namespace INDT.Common.Insurance.Infra.Interfaces
{
    public interface IStorageClientService
    {

        Task<Result> UploadFile(Microsoft.AspNetCore.Http.IFormFile file);
    }
}
