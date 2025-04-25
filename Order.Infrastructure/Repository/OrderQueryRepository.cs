using Order.Application.Queries;
using Order.Infrastructure.DataContext;

namespace Order.Infrastructure.Repository
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private readonly QueryDbContext queryDbContext;

        public OrderQueryRepository(QueryDbContext queryDbContext)
        {
            this.queryDbContext = queryDbContext;
        }

        public Task GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetList()
        {
            throw new NotImplementedException();
        }
    }
}
