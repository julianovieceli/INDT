using Insurance.INDT.Infra.MongoDb.Repository.Domain;
using Insurance.INDT.Infra.MongoDb.Repository.Interface;
using Personal.Common.Infra.MongoDb.Repository.Interfaces;
using Personal.Common.Infra.MongoDb.Repository.Repository;

namespace Insurance.INDT.Infra.MongoDb.Repository
{
    public class ClientDocumentRepository : MongoDbRepositoryBase<ClientDocument>, IClientDocumentRepository
    {
        public ClientDocumentRepository(IMongoDbcontext dbcontext) : base(dbcontext, "Client")
        {

        }

        public async Task<ClientDocument> GetByDocto(string docto)
        {
            try
            {
                return await base.FindAsync(d => d.Docto == docto);
            }
            catch
            (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> InsertAsync(ClientDocument clientDocument)
        {
            try
            {
                return await base.InsertAsync(clientDocument);
            }
            catch
            (Exception ex)
            {
                throw;
            }
        }
    }
}
