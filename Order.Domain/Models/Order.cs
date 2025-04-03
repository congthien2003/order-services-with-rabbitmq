namespace Order.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
