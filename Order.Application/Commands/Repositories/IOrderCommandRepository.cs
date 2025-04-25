namespace Order.Application.Commands.Repositories
{
    public interface IOrderCommandRepository
    {
        Task<Guid> Create(Order.Domain.Models.Order order);
        Task<Guid> Update(Order.Domain.Models.Order order);
        Task<Guid> UpdateStatusAsync(Guid orderId, string status);
        Task<bool> Delete(Guid id);
    }
}
