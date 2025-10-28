using INDT.Common.Insurance.Infra.Interfaces.Rabbit;
using Insurance.INDT.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Insurance.INDT.Application.Messaging.Rabbit
{
    public class RabbitMqConsumerService : IRabbitMqConsumerService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqConsumerService> _logger;
        private const string QueueName = "QueueTest"; // Replace with your queue name

        public RabbitMqConsumerService(IConnection connection, ILogger<RabbitMqConsumerService> logger)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _logger = logger;
        }

          public void StartConsuming()
        {
            _logger.LogInformation("RabbitMQ consumer started.");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Received message: {message}");
                // Process the message here
                _channel.BasicAck(ea.DeliveryTag, false); // Acknowledge message
            };
            _channel.BasicConsume(queue: QueueName,
                                 autoAck: false, // Handle acknowledgment manually
                                 consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            
        }
    }

    public static class IoCConsumer
    {

        public static IServiceCollection AddRabbitMqConsumerService(this IServiceCollection services,  IConfiguration configuration)
        {
            RabbitMqConfig rabbitMqConfig = new RabbitMqConfig();
            configuration.GetSection(nameof(RabbitMqConfig)).Bind(rabbitMqConfig);


            services.AddSingleton<IConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = rabbitMqConfig.HostName, 
                    Port = rabbitMqConfig.Port,
                    UserName = rabbitMqConfig.User, 
                    Password = rabbitMqConfig.Pwd
                };
                return factory.CreateConnection();
            });

            services.AddScoped<IRabbitMqConsumerService, RabbitMqConsumerService>();

            return services;
        }

       



    }
}
