using Insurance.INDT.Infra.MongoDb.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;



namespace Insurance.INDT.Infra.MongoDb.Repository
{
    public static class Ioc
    {
        public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IClientDocumentRepository , ClientDocumentRepository>();

        }
    }
}
