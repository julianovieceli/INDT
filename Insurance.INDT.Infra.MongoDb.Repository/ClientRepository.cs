using INDT.Common.Insurance.Domain.Interfaces.Repository.Mongo;
using INDT.Common.Insurance.Infra.MongoDb.Repository;

namespace Insurance.INDT.Infra.MongoDb.Repository
{
    public class ClientRepository : MongoDbRepositoryBase<ClientDocument>
    {
        public ClientRepository(IMongoDbcontext dbcontext) : base(dbcontext, "Client")
        {
        }
    }
}
