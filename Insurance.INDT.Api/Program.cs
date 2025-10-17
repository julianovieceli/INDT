using Insurance.INDT.Application;
using Insurance.INDT.Infra.Mysql.Repository;
using Insurance.Proposal.INDT.Api;
using INDT.Common.Insurance.Infra.MongoDb.Repository;

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
builder.Services.InitializeDataMapper();
builder.Services.AddServiceBus(builder.Configuration);
builder.Services.AddBackgroundServices(builder.Configuration);
builder.Services.AddMongoDbContext(
    builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value!,
    builder.Configuration.GetSection("MongoDbSettings:Database").Value!
    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
