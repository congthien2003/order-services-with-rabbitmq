using MassTransit;
using MediatR;
using Order.Application.Commands.Repositories;
using Order.Application.Events;

namespace Order.Application.Commands
{
    public sealed class CreateOrderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public Guid CustomerId { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderCommandRepository _orderCommandRepository;
        private readonly IRabbitMQService rabbitMQService;
        private readonly IPublishEndpoint publisher;
        public CreateOrderCommandHandler(IOrderCommandRepository orderCommandRepository,
                                         IRabbitMQService rabbitMQService,
                                            IPublishEndpoint publisher)
        {
            _orderCommandRepository = orderCommandRepository;
            this.rabbitMQService = rabbitMQService;
            this.publisher = publisher;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = new Domain.Models.Order
            {
                Id = request.Id,
                Total = request.Total,
                CustomerId = request.CustomerId,
            };

            var orderId = await _orderCommandRepository.Create(newOrder);

            var events = new OrderCreatedEvent(orderId, newOrder.Total, newOrder.CustomerId);

            publisher.Publish(events, cancellationToken);
            //await rabbitMQService.PublishMessageAsync(events);

            return orderId;
        }
    }
}
