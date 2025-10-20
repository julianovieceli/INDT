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

        public static IServiceCollection AddAWSClient(this IServiceCollection services, IConfiguration configuration)
        {
            AmazonSqSConfig amazonSQSConfig = new AmazonSqSConfig();
            configuration.GetSection("AmazonSQSConfig").Bind(amazonSQSConfig);

            services.AddSingleton<IAmazonSQS>(sp =>
            {
                var config = new AmazonSQSConfig
                {
                    ServiceURL = amazonSQSConfig.ServiceURL,
                    AuthenticationRegion = amazonSQSConfig.AuthenticationRegion
                };
                return new AmazonSQSClient(amazonSQSConfig.awsAccessKeyId, amazonSQSConfig.awsSecretAccessKey, config); // Dummy credentials
            });

            services.AddHostedService<SqsMessageReceiverServiceWorker>();


            return services;
        }
    }
}
