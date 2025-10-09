using Insurance.INDT.Domain.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.INDT.Repository
{
    public static class Ioc
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}
