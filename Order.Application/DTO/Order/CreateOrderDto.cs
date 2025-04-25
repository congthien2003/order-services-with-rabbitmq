namespace Order.Application.DTO.Order
{
    public class CreateOrderDto
    {
        public decimal Total { get; set; }
        public Guid CustomerId { get; set; }
    }

}
