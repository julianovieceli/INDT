using Insurance.INDT.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ServiceBusClientReceiverBackGroundServiceWorker>();

var host = builder.Build();
host.Run();
