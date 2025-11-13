
using AutoMapper;
using Insurance.ProposalHire.INDT.Application.Mapping;
using Personal.Common.Infra.Mysql.Repository.Settings;

namespace Insurance.ProposalHire.INDT.Api;

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
            cfg.AddProfile<ProposalHireProfile>();
            cfg.AddProfile<ProposalProfile>();
        });

        IMapper mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        return services;


    }
}
