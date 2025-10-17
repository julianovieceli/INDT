using INDT.Common.Insurance.Domain;
using Insurance.INDT.Infra.MongoDb.Repository.Domain;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IClientDomainService
    {
        Task<Result> InsertClient(ClientDocument clientDocument);

    }
}
