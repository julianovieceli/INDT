using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.INDT.Application
{
    public static class Ioc
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddScoped<IClientService, ClientService>();
        }
    }
}
