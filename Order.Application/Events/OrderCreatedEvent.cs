using Order.Domain.Abstractions;

namespace Order.Application.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public OrderPayload Payload { get; set; }

        public OrderCreatedEvent() : base(eventType: "OrderCreated", source: "order-service") { }
        public OrderCreatedEvent(Guid orderId, string description, decimal price, int quantity, Guid customerId)
            : base(eventType: "OrderCreated", source: "order-service")
        {
            Payload = new OrderPayload
            {
                OrderId = orderId,
                Description = description,
                Price = price,
                Quantity = quantity,
                CustomerId = customerId
            };
        }
    }

    public class OrderPayload
    {
        public Guid OrderId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CustomerId { get; set; }
    }

}
