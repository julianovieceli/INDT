using INDT.Common.Insurance.Application.Services;
using INDT.Common.Insurance.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static BasicAuthenticationHandler;

namespace INDT.Common.Insurance.Application
{
    public static class IoC
    {
        public static IServiceCollection AddJwtConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }


        public static IServiceCollection AddBasicAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UserAuthenticationSettings>(configuration.GetSection(nameof(UserAuthenticationSettings)));

            services.AddAuthentication(BasicAuthenticationOptions.DefaultScheme)
            .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(BasicAuthenticationOptions.DefaultScheme, options => { });

            services.AddAuthorizationCore();


            return services;
        }
    }
}
