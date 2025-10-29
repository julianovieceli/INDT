using INDT.Common.Insurance.Application.Services;
using INDT.Common.Insurance.Application.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });

            services.AddAuthorizationCore();


            return services;
        }
    }
}
