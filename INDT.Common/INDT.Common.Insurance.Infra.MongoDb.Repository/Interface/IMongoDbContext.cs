using MongoDB.Driver;

namespace INDT.Common.Insurance.Domain.Interfaces.Repository.Mongo
{
    public interface IMongoDbcontext
    {
        IMongoDatabase DataBase { get; }
    }
}
