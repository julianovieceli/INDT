using Insurance.INDT.Infra.MongoDb.Repository.Domain;

namespace Insurance.INDT.Infra.MongoDb.Repository.Interface
{
    public interface IClientDocumentRepository
    {
        Task<ClientDocument> GetByDocto(string docto);

        Task<bool> InsertAsync(ClientDocument clientDocument);
    }
}
