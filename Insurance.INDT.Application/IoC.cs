using Amazon.SQS;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using FluentValidation;
using INDT.Common.Insurance.Application.Validators;
using INDT.Common.Insurance.Dto.Request;
using Insurance.INDT.Application.Api;
using Insurance.INDT.Application.Mapping;
using Insurance.INDT.Application.ServiceBus;
using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Application.Settings;
using Insurance.INDT.Application.Validators;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.INDT.Application
{
    public static class IoC
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProposalService, ProposalService>();
            services.AddScoped<IServiceBusClientService, ServiceBusClientService>();

            services.AddScoped<IClientDomainService, ClientDomainService>();

            services.AddScoped<IApiWebhookSenderService, ApiWebhookSenderService>();

            return services.AddScoped<IClientService, ClientService>();
        }

        public static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {

            
            services.AddOptions<ServiceBusSettings>().BindConfiguration("ServiceBus");

            //services.AddHostedService<ServiceBusMessageReceiverService>();

            return services;
        }


            public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterClientDto>, RegisterClientDtoValidator>();
            services.AddScoped<IValidator<RegisterProposalDto>, RegisterProposalValidator>();
            return services.AddScoped<IValidator<RegisterInsuranceDto>, RegisterInsuranceDtoValidator>();
        }

        public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            var connStringSection = configuration.GetSection("ServiceBus:ServiceBusConnection");
            
            var topic = configuration.GetSection("ServiceBus:TopicName");

            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(connStringSection.Value);


                builder.AddClient<ServiceBusSender, ServiceBusSenderOptions>((options, provider) =>
                {
                    var serviceBusClient = provider.GetRequiredService<ServiceBusClient>();
                    return serviceBusClient.CreateSender(topic.Value);
                }).WithName("TesteMensagem");
            }); 


            return services;
        }

       


        public static IServiceCollection AddHttpClient(this IServiceCollection services
           , IConfiguration configuration)
        {
            services.Configure<WebhookSettings>(configuration.GetSection("WebhookSettings"));
            
            WebhookSettings webhookSettings = new WebhookSettings();
            configuration.GetSection("WebhookSettings").Bind(webhookSettings);


            services.AddHttpClient(nameof(HttpClientEnum.API_WEBHOOK), httpClient =>
            {
                httpClient.BaseAddress = new Uri(webhookSettings.BaseUrl);
            });

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {


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
