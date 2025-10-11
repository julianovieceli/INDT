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
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProposalService, ProposalService>();
            return services.AddScoped<IClientService, ClientService>();
        }


        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterClientDto>, RegisterClientDtoValidator>();
            return services.AddScoped<IValidator<RegisterInsuranceDto>, RegisterInsuranceDtoValidator>();
        }
    }
}
