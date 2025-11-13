using INDT.Common.Insurance.Domain.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using Personal.Common.Infra.Mysql.Repository;



namespace Insurance.INDT.Infra.Mysql.Repository
{
    public static class Ioc
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IInsuranceRepository, InsuranceRepository>();

            services.AddMySqlDbContext();

            return services.AddScoped<IProposalRepository, ProposalRepository>();
        }
    }
}
