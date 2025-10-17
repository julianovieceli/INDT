using AutoMapper;
using Azure.Messaging.ServiceBus;
using INDT.Common.Insurance.Domain;
using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Application.Settings;
using Insurance.INDT.Infra.MongoDb.Repository.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

public class ServiceBusMessageReceiverService : BackgroundService
{

    private readonly ILogger<ServiceBusMessageReceiverService> _logger;

    private readonly ServiceBusSettings _serviceBusSettings;
    private ServiceBusClient _client;
    private ServiceBusProcessor _processor;
    private readonly IMapper _dataMapper;

    private readonly IServiceProvider _serviceProvider;



    public ServiceBusMessageReceiverService(ILogger<ServiceBusMessageReceiverService> logger, IServiceProvider serviceProvider,
        IMapper dataMapper, IOptions<ServiceBusSettings> serviceBusSettings)
    {
        
        _serviceBusSettings = serviceBusSettings.Value;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _dataMapper = dataMapper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Service Bus message ExecuteAsync service running.");
        _logger.LogInformation($"Sbus connection :{_serviceBusSettings.ServiceBusConnection}");
        _logger.LogInformation($"Sbus Topic :{_serviceBusSettings.TopicName}");
        _logger.LogInformation($"Sbus subscription :{_serviceBusSettings.SubscriptionName}");

        _client = new ServiceBusClient(_serviceBusSettings.ServiceBusConnection);
        _processor = _client.CreateProcessor(_serviceBusSettings.TopicName, _serviceBusSettings.SubscriptionName, new ServiceBusProcessorOptions());

        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;

        await _processor.StartProcessingAsync(stoppingToken);

        // Keep the service running until cancellation is requested
        await Task.Delay( Timeout.Infinite, stoppingToken);

        _logger.LogInformation("ServiceBusMessageReceiverService is stopping.");


    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();

        _logger.LogInformation($"Received message: ID = {args.Message.MessageId}, Body = {body}");


        Client client = JsonSerializer.Deserialize<Client>(body);

        ClientDocument cd = _dataMapper.Map<ClientDocument>(client);

        using (var scope = _serviceProvider.CreateScope())
        {
            var clientDomainService = scope.ServiceProvider.GetRequiredService<IClientDomainService>();
            await clientDomainService.InsertClient(cd);
        }


        // Complete the message to remove it from the queue
        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {

        _logger.LogError(args.Exception.Message, "Error processing Service Bus message.");
        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_processor != null)
        {
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
        }
        if (_client != null)
        {
            await _client.DisposeAsync();
        }
        await base.StopAsync(cancellationToken);
    }
}