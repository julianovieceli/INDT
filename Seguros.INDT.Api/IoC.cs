
using Insurance.INDT.Mysql.Repository.Settings;

namespace Insurance.Proposal.INDT.Api
{
    public static class Ioc
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlDbcontextSettings>(configuration.GetSection("MySqlDbcontext"));

            return services;
        }
    }
}
