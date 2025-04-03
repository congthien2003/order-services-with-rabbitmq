using Microsoft.AspNetCore.Mvc;
using Order.Application.DTO.Order;
using Order.Application.Interfaces;
using SharedLib;
namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<Result<Guid>> CreateOrderAsync([FromBody] CreateOrderDto dto)
        {
            var orderId = await _orderService.CreateOrderAsync(dto);
            return Result<Guid>.Success("Order created!", orderId);
        }
    }
}
