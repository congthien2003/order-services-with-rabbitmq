using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.CreateOrder;
using Order.Application.DTO.Order;
using Order.Application.Events;
using Order.Application.Interfaces;
using Order.Infrastructure.MessageBroker;

namespace Order.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderService> _logger;
        private readonly IRabbitMQService _rabbitMQService;

        public OrderService(IMediator mediator,
            ILogger<OrderService> logger,
            IRabbitMQService rabbitMQService)
        {
            _mediator = mediator;
            _logger = logger;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDto create)
        {
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            // Gửi command qua MediatR để xử lý
            var command = new CreateOrderCommand
            {
                Id = orderId,
                Description = create.Description,
                Price = create.Price,
                Quantity = create.Quantity,
                CustomerId = customerId,
            };

            await _mediator.Send(command);

            _logger.LogInformation($"Order created: {orderId}");

            // Gửi message qua RabbitMQ
            var message = new OrderCreatedEvent(orderId, create.Description, create.Price, create.Quantity, create.CustomerId);
            await _rabbitMQService.PublishMessageAsync(message);

            return orderId;
        }
    }

}
