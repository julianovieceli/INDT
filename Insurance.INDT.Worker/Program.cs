using INDT.Common.Insurance.Infra.MongoDb.Repository;
using Insurance.INDT.Application;
using Insurance.INDT.Application.Messaging.AWS;
using Insurance.INDT.Application.Messaging.Azure;
using Insurance.INDT.Application.Messaging.Rabbit;
using Insurance.INDT.Infra.MongoDb.Repository;
using Insurance.INDT.Worker;
using Insurance.INDT.Application.Servless.AWS;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAzureMessagingClientService(builder.Configuration);
builder.Services.AddAWSMessagingClientService(builder.Configuration);
builder.Services.AddRabbitMqConsumerService(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddWorkerApplicationServices();
builder.Services.AddHttpClient(builder.Configuration);
builder.Services.AddBackgroundServices(builder.Configuration);
builder.Services.AddMongoDbContext(
    builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value!,
    builder.Configuration.GetSection("MongoDbSettings:Database").Value!
    );
builder.Services.AddMongoDbRepositories();
builder.Services.AddAWSLambdaClientService();
builder.Services.AddHostedService<ServiceBusClientReceiverBackGroundServiceWorker>();
builder.Services.AddHostedService<SqsMessageReceiverServiceWorker>();
builder.Services.AddHostedService<RabbitMqMessageReceiverServiceWorker>();
builder.Services.AddHostedService<AwsLambdaFunctionClientServiceWorker>();



var host = builder.Build();
host.Run();
