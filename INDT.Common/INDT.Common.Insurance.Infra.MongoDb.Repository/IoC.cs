using INDT.Common.Insurance.Domain.Interfaces.Repository.Mongo;
using Microsoft.Extensions.DependencyInjection;



namespace INDT.Common.Insurance.Infra.MongoDb.Repository
{
    public static class Ioc
    {
        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, string connString, string database)
        {

            return services.AddSingleton<IMongoDbcontext>(s =>
            {
                return new MongoDbContext(connString, database);
                
            });
        
        }
    }
}
