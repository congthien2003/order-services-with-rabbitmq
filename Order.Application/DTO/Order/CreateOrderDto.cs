namespace Order.Application.DTO.Order
{
    public class CreateOrderDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CustomerId { get; set; }
    }

}
