using MediatR;
using Order.Application.Commands.Repositories;

namespace Order.Application.Commands
{
    public sealed class DeleteOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderCommandRepository _orderCommandRepository;
        public DeleteOrderCommandHandler(IOrderCommandRepository orderCommandRepository)
        {
            _orderCommandRepository = orderCommandRepository;
        }
        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _orderCommandRepository.Delete(request.Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
