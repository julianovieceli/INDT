using Insurance.INDT.Application.Services;
using Insurance.ProposalHire.INDT.Application.Api;
using Insurance.ProposalHire.INDT.Application.Services.Interfaces;
using Insurance.ProposalHire.INDT.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.ProposalHire.INDT.Application
{
    public static class Ioc
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProposalHireService, ProposalHireService>();
            return services.AddScoped<IApiProposalService, ApiProposalService>();
        }


        public static IServiceCollection AddHttpClient(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.Configure<ProposalUrlSettings>(configuration.GetSection("ProposalUrlSettings"));

            ProposalUrlSettings proposalUrlSettings = new ProposalUrlSettings();
            configuration.GetSection("ProposalUrlSettings").Bind(proposalUrlSettings);


            services.AddHttpClient(nameof(HttpClientEnum.API_APPROVAL), httpClient =>
            {
                httpClient.BaseAddress = new Uri(proposalUrlSettings.BaseUrl);
            });

            return services;
        }



    }
}
