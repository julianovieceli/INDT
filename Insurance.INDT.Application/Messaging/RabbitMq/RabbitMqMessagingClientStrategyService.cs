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
    public class RabbitMqMessagingClientStrategyService : IRabbitMqMessagingClientStrategyService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqMessagingClientStrategyService> _logger;
        private string exchangeName = "ExchangeTestDirect";
        private const string QueueName = "QueueTest"; // Replace with your queue name

        public RabbitMqMessagingClientStrategyService(IConnection connection, ILogger<RabbitMqMessagingClientStrategyService> logger)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _logger = logger;
        }

        public async Task SendMessage<T>(T msg)
        {
            try
            {

                _channel.QueueDeclare(queue: QueueName,
                               durable: true, // Queue survives broker restarts
                               exclusive: false, // Only accessible by the current connection
                               autoDelete: false, // Deleted when last consumer unsubscribes
                               arguments: null);



                string messageBody = JsonSerializer.Serialize(msg);

                var message = Encoding.UTF8.GetBytes(messageBody);

                IBasicProperties props = _channel.CreateBasicProperties();
                props.MessageId = Guid.NewGuid().ToString(); // Generate a unique ID

                _channel.BasicPublish(exchange: "",
                                routingKey: QueueName,
                                basicProperties: props,
                    body: message);


                _logger.LogInformation($"Message sent To RabbitMq successfully to queue '{QueueName}'.Message ID: {props.MessageId}");

            }
                  catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }

        }

        
        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            
        }
    }

    public static class IoC
    {

        public static IServiceCollection AddRabbitMqClientService(this IServiceCollection services,  IConfiguration configuration)
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

            services.AddScoped<IRabbitMqMessagingClientStrategyService, RabbitMqMessagingClientStrategyService>();

            return services;
        }

       



    }
}
