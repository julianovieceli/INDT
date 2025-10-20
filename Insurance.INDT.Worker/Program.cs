using Insurance.INDT.Application;
using Insurance.INDT.Worker;
using Insurance.INDT.Infra.MongoDb.Repository;
using INDT.Common.Insurance.Infra.MongoDb.Repository;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddServiceBus(builder.Configuration);
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

var host = builder.Build();
host.Run();
