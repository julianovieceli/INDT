using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Mysql.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.INDT.Repository
{
    public static class Ioc
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();

            
            return services.AddScoped<IDbContext, MySqlDbcontext>();
        }
    }
}
