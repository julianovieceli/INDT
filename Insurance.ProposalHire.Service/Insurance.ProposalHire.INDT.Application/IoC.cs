using Insurance.INDT.Application.Services;
using Insurance.ProposalHire.INDT.Application.Api;
using Insurance.ProposalHire.INDT.Application.Services.Interfaces;
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


      
    }
}
