using FluentValidation;
using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Application.Validators;
using Insurance.INDT.Dto.Request;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.INDT.Application
{
    public static class Ioc
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddScoped<IClientService, ClientService>();
        }


        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services.AddScoped<IValidator<RegisterClientDto>, RegisterClientDtoValidator>();
        }
    }
}
