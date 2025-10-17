using INDT.Common.Insurance.Domain.Interfaces.Repository.Mongo;
using MongoDB.Driver;

namespace INDT.Common.Insurance.Infra.MongoDb.Repository
{
    public class MongoDbContext: IMongoDbcontext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoDatabase DataBase
        {
            get { return _database; }
        }
    }
}
