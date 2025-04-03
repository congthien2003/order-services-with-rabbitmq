using MediatR;
using Order.Domain.Repository;

namespace Order.Application.CreateOrder
{
    public sealed class CreateOrderCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CustomerId { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository<Domain.Models.Order> _orderRepository;
        public CreateOrderCommandHandler(IOrderRepository<Domain.Models.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        async Task IRequestHandler<CreateOrderCommand>.Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = new Domain.Models.Order
            {
                Id = request.Id,
                Description = request.Description,
                Price = request.Price,
            };
            await _orderRepository.Create(newOrder);
            Console.WriteLine("Order created!");
        }
    }
}
