using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Order.Application.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
namespace Order.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ConnectionFactory factory;
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger)
        {
            factory = new ConnectionFactory { HostName = "localhost" };
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await channel.QueueDeclareAsync(queue: "order", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);

                    // 🛠 Parse JSON thành OrderCreatedEvent
                    var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(messageJson);
                    if (orderEvent == null)
                    {
                        _logger.LogError("Failed to parse OrderCreatedEvent");
                        return Task.CompletedTask;
                    }

                    _logger.LogInformation($" [✔] Received OrderCreatedEvent: {orderEvent.Payload.OrderId}");


                    return Task.CompletedTask;
                };

                await channel.BasicConsumeAsync("order", autoAck: true, consumer: consumer);

                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}
