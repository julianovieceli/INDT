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
  


        //public static IServiceCollection AddBasicAuthentication(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.Configure<UserAuthenticationSettings>(configuration.GetSection(nameof(UserAuthenticationSettings)));


        //    string dockerPath = "/app/secrets";
        //    if (Directory.Exists(dockerPath))
        //    {
        //        var secretPassValue = File.ReadAllText(dockerPath + "/" + "user_authentication_pass");
        //        var secretJwtValue = File.ReadAllText(dockerPath + "/" + "jwt_secret");
                
        //        services.PostConfigure<UserAuthenticationSettings>((user) =>
        //        {
        //            user.Password = secretPassValue;
        //        }); ;

        //        services.PostConfigure<JwtSettings>((jwt) =>
        //        {
        //            jwt.Key = secretJwtValue;
        //        }); ;
        //    }

        //    services.AddAuthentication(BasicAuthenticationOptions.DefaultScheme)
        //    .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(BasicAuthenticationOptions.DefaultScheme, options => {

                
        //    });

        //    services.AddAuthorizationCore();


        //    return services;
        //}

        //public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        //{
        //    string jwtKey = configuration["JwtSettings:Key"];

        //    string dockerPath = "/app/secrets";
        //    if (Directory.Exists(dockerPath))
        //        jwtKey = File.ReadAllText(dockerPath + "/" + "jwt_secret");


        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //        .AddJwtBearer( options =>
        //    {
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = configuration["JwtSettings:Issuer"],
        //            ValidAudience = configuration["JwtSettings:Audience"],
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        //        };

        //        options.Events = new JwtBearerEvents
        //        {
        //            OnMessageReceived = context =>
        //            {
        //                // Log when a token is received
        //                Console.WriteLine($"Token received: {context.Token}");
        //                return Task.CompletedTask;
        //            },
        //            OnAuthenticationFailed = context =>
        //            {
        //                // Log authentication failures
        //                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
        //                return Task.CompletedTask;
        //            },
        //            OnTokenValidated = context =>
        //            {
        //                // Log successful token validation
        //                Console.WriteLine($"Token validated for user: {context.Principal?.Identity?.Name}");
        //                return Task.CompletedTask;
        //            }
        //        };
        //    });

        //    services.AddAuthorizationCore();


        //    return services;
        //}
    }
}
