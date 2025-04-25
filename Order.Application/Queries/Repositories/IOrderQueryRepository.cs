namespace Order.Application.Queries
{
    public interface IOrderQueryRepository
    {
        Task GetList();
        Task GetById(Guid id);
    }
}
