using Microsoft.Extensions.Logging;
using Order.Domain.Abstractions;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Order.Infrastructure.MessageBroker
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConnectionFactory _factory;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(ILogger<RabbitMQService> logger)
        {
            _factory = new ConnectionFactory();
            _logger = logger;
        }

        public async Task PublishMessageAsync(string queue, string message)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: body);

            _logger.LogInformation($" [x] Sent {message}");
        }

        public async Task PublishMessageAsync<T>(T eventMessage) where T : BaseEvent
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            //await channel.QueueDeclareAsync(queue: eventMessage.EventType, durable: true, exclusive: false, autoDelete: false, arguments: null);
            await channel.QueueDeclareAsync(queue: "order", durable: false, exclusive: false, autoDelete: false,
                arguments: null);
            var jsonMessage = JsonSerializer.Serialize(eventMessage);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "order", body: body);

            _logger.LogInformation($" [x] Sent {eventMessage.EventType}: {jsonMessage}");
        }
    }

}
