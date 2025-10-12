using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Infra.Mysql.Repository;
using Microsoft.Extensions.DependencyInjection;



namespace Insurance.ProposalHire.INDT.MySql.Repository
{
    public static class Ioc
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
         
            services.AddScoped<IProposalHireRepository, ProposalHireRepository>();
            

            return services.AddScoped<IDbContext, MySqlDbcontext>();
        }
    }
}
