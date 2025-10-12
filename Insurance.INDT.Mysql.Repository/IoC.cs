using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Infra.Mysql.Repository;
using Microsoft.Extensions.DependencyInjection;



namespace Insurance.INDT.Infra.Mysql.Repository
{
    public static class Ioc
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IInsuranceRepository, InsuranceRepository>();

            services.AddScoped<IProposalRepository, ProposalRepository>();

            

            return services.AddScoped<IDbContext, MySqlDbcontext>();
        }
    }
}
