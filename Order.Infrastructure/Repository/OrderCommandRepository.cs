using Order.Application.Commands.Repositories;
using Order.Infrastructure.DataContext;

namespace Order.Infrastructure
{
    public class OrderCommandRepository : IOrderCommandRepository
    {
        private readonly CommandDbContext _commandContext;

        public OrderCommandRepository(CommandDbContext commandContext)
        {
            _commandContext = commandContext;
        }

        public Task<Guid> Create(Domain.Models.Order order)
        {
            _commandContext.Orders.Add(order);
            _commandContext.SaveChanges();
            return Task.FromResult(order.Id);
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Update(Domain.Models.Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateStatusAsync(Guid orderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
