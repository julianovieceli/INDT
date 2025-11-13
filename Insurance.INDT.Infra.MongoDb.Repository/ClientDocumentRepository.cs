using Insurance.INDT.Infra.MongoDb.Repository.Domain;
using Insurance.INDT.Infra.MongoDb.Repository.Interface;
using Microsoft.Extensions.Logging;
using Personal.Common.Infra.MongoDb.Repository;
using Personal.Common.Infra.MongoDb.Repository.Interfaces;

namespace Insurance.INDT.Infra.MongoDb.Repository
{
    public class ClientDocumentRepository : MongoDbRepositoryBase<ClientDocument>, IClientDocumentRepository
    {
        
        public ClientDocumentRepository(IMongoDbcontext dbcontext, ILogger<ClientDocument> logger) : base(dbcontext, "Client", logger)
        {

        }

        public async Task<ClientDocument> GetByDocto(string docto)
        {
            try
            {
                return await base.FindOneAsync(d => d.Docto == docto);
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
