using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Mysql.Repository;
using Microsoft.Extensions.DependencyInjection;



namespace Insurance.INDT.Repository
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
