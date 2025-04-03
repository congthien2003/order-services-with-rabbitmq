using Order.Domain.Repository;

namespace Order.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository<Domain.Models.Order>
    {
        public Task<Domain.Models.Order> Create(Domain.Models.Order entity)
        {
            return Task.FromResult(entity);
        }

        public Task<Domain.Models.Order> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Models.Order> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Models.Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Models.Order> Update(Domain.Models.Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
