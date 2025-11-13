using Insurance.INDT.Infra.MongoDb.Repository.Domain;
using Personal.Common.Domain;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IClientDomainService
    {
        Task<Result> InsertClient(ClientDocument clientDocument);

    }
}
