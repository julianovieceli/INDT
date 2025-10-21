using Amazon.SQS;
using Insurance.INDT.Application.Api;
using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Application.Settings;

namespace Insurance.INDT.Worker
{
    public static  class IoC
    {
        public static IServiceCollection AddWorkerApplicationServices(this IServiceCollection services)
        {
         
            services.AddScoped<IClientDomainService, ClientDomainService>();

            return services.AddScoped<IApiWebhookSenderService, ApiWebhookSenderService>();
        }

      
    }
}
