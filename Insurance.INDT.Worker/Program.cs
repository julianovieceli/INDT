using Insurance.INDT.Application;
using Insurance.INDT.Worker;
using Insurance.INDT.Infra.MongoDb.Repository;
using INDT.Common.Insurance.Infra.MongoDb.Repository;
using Insurance.INDT.Application.ServiceBus.AWS;
using Insurance.INDT.Application.ServiceBus.Azure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAzureMessagingClientService(builder.Configuration);
builder.Services.AddAWSMessagingClientService(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddWorkerApplicationServices();
builder.Services.AddHttpClient(builder.Configuration);
builder.Services.AddBackgroundServices(builder.Configuration);
builder.Services.AddMongoDbContext(
    builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value!,
    builder.Configuration.GetSection("MongoDbSettings:Database").Value!
    );
builder.Services.AddMongoDbRepositories();
builder.Services.AddHostedService<ServiceBusClientReceiverBackGroundServiceWorker>();
builder.Services.AddHostedService<SqsMessageReceiverServiceWorker>();


var host = builder.Build();
host.Run();
