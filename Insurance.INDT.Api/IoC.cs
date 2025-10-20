
using AutoMapper;
using INDT.Common.Insurance.Infra.Mysql.Repository;
using Insurance.INDT.Application.Mapping;

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
