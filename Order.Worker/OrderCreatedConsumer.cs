using MassTransit;
using Microsoft.Extensions.Logging;
using Order.Application.Events;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
        _logger = logger;
        Console.WriteLine("Order Consumer created !!!");
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var order = context.Message;
        _logger.LogInformation($"[✔] Consumed OrderCreated: {order.Payload.OrderId}, Total: {order.Payload.Total}");

        // xử lý gì đó ở đây, ví dụ lưu vào QueryDbContext
    }
}
