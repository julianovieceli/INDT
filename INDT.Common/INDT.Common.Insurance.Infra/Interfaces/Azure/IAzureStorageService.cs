using Personal.Common.Domain;

namespace INDT.Common.Insurance.Infra.Interfaces.Azure
{
    public interface IAzureStorageService : IStorageClientService
    {
        Task<Result> DownloadFile(string fileName);
    }
}
