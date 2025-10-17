using MongoDB.Bson;

namespace INDT.Common.Insurance.Infra.MongoDb.Repository
{
    public abstract class MongoDbEntityBase
    {
        public ObjectId Id { get; set; }
    }
}
