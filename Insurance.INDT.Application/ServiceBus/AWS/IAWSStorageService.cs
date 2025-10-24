using INDT.Common.Insurance.Domain;

namespace Insurance.INDT.Application.ServiceBus.AWS
{
    public interface IAWSStorageService
    {
        Task<Result> UploadFile(Microsoft.AspNetCore.Http.IFormFile file);
    }
}
