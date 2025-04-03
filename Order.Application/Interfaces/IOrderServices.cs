using Order.Application.DTO.Order;

namespace Order.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(CreateOrderDto dto);
    }

}
