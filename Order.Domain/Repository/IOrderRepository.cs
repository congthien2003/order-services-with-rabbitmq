namespace Order.Domain.Repository
{
    public interface IOrderRepository<T> where T : Models.Order
    {
        IEnumerable<T> GetAll();
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
