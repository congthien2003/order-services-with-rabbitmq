using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands;
using Order.Application.DTO.Order;
using SharedLib;
namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<Guid>> CreateOrderAsync([FromBody] CreateOrderDto dto)
        {
            CreateOrderCommand cmd = new CreateOrderCommand()
            {
                Id = Guid.NewGuid(),
                Total = dto.Total,
                CustomerId = dto.CustomerId
            };
            var orderId = await mediator.Send(cmd);
            return Result<Guid>.Success("Order created!", orderId);
        }
    }
}
