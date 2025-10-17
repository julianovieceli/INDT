using INDT.Common.Insurance.Domain.Interfaces.Repository.Mongo;
using INDT.Common.Insurance.Infra.MongoDb.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace INDT.Common.Insurance.Infra.MongoDb.Repository
{
    public class MongoDbRepositoryBase<TEntity> : IMongoDbRepositoryBase<TEntity> where TEntity : MongoDbEntityBase
    {

        private readonly IMongoCollection<TEntity> _collection;

        public MongoDbRepositoryBase(IMongoDbcontext dbcontext, string collectionName)
        {
            _collection = dbcontext.DataBase.GetCollection<TEntity>(collectionName);
        }
        public async Task<bool> InsertAsync(TEntity mongoDbEntityBase)
        {
            try
            {
                await _collection.InsertOneAsync(mongoDbEntityBase);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TEntity> GetAsync(ObjectId id)
        {
            try
            {
                return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }

        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            try
            {
                return await _collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }
        }


        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return await _collection.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                {
                    throw;
                }
            }
        }
    }
}
