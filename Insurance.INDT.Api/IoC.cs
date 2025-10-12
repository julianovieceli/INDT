
using AutoMapper;
using INDT.Common.Insurance.Infra.Mysql.Repository;
using Insurance.INDT.Application.Mapping;

namespace Insurance.Proposal.INDT.Api
{
    public static class Ioc
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlDbcontextSettings>(configuration.GetSection("MySqlDbcontext"));

            return services;
        }


        public static IServiceCollection InitializeDataMapper(this IServiceCollection services) { 
         

           var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClientProfile>();
                cfg.AddProfile<InsuranceProfile>();
                cfg.AddProfile<ProposalProfile>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
