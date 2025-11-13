using Personal.Common.Infra.Mysql.Repository.Settings;

namespace Insurance.Proposal.INDT.Api
{
    public static class IoC
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlDbcontextSettings>(configuration.GetSection("MySqlDbcontext"));

            return services;
        }


        
    }
}
