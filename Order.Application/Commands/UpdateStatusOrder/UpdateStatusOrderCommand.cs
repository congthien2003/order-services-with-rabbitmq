using MediatR;
using Order.Application.Commands.Repositories;
using Order.Domain.Enum;

namespace Order.Application.Commands.UpdateStatusOrder
{
    public class UpdateStatusOrderCommand : IRequest
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Total { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class UpdateStatusOrderCommandHandler : IRequestHandler<UpdateStatusOrderCommand>
    {
        private readonly IOrderCommandRepository orderCommandRepository;

        public UpdateStatusOrderCommandHandler(IOrderCommandRepository orderCommandRepository)
        {
            this.orderCommandRepository = orderCommandRepository;
        }

        public async Task Handle(UpdateStatusOrderCommand request, CancellationToken cancellationToken)
        {
            var updateOrder = new Domain.Models.Order
            {
                Id = request.Id,
                Status = request.OrderStatus,
                Total = request.Total,
                CustomerId = request.CustomerId,
            };
            await orderCommandRepository.Update(updateOrder);
        }
    }
}
