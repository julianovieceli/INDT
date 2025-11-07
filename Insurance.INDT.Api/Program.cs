using INDT.Common.Insurance.Application;
using Insurance.INDT.Application;
using Insurance.INDT.Application.Messaging.AWS;
using Insurance.INDT.Application.Messaging.Azure;
using Insurance.INDT.Application.Messaging.Rabbit;
using Insurance.INDT.Application.Storage.Azure;
using Insurance.INDT.Infra.MongoDb.Repository;
using Insurance.INDT.Infra.Mysql.Repository;
using Insurance.Proposal.INDT.Api;
using Microsoft.IdentityModel.Logging;
using Personal.Common.MongoDb.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSettings(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddRepositories();
builder.Services.AddValidators();
builder.Services.AddAutoMapper();
builder.Services.AddAzureMessagingClientService(builder.Configuration);
builder.Services.AddAWSMessagingClientService(builder.Configuration);
builder.Services.AddRabbitMqClientService(builder.Configuration);
builder.Services.AddAWSStorageService(builder.Configuration);
builder.Services.AddAzureStorageService(builder.Configuration);
builder.Services.AddBackgroundServices(builder.Configuration);

builder.Services.AddMongoDbContext(
    builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value!,
    builder.Configuration.GetSection("MongoDbSettings:Database").Value!
    );
builder.Services.AddMongoDbRepositories();
builder.Services.AddHttpClient(builder.Configuration);
builder.Services.AddHealthChecks();

builder.Services.AddJwtConfigurations(builder.Configuration);
builder.Services.AddBasicAuthentication(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable PII logging only in development environments
    IdentityModelEventSource.ShowPII = true;

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health"); // Exposes a health check endpoint at /health

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
