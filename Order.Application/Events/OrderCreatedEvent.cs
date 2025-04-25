using Order.Domain.Abstractions;

namespace Order.Application.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public OrderPayload Payload { get; set; }

        public OrderCreatedEvent() : base(eventType: "OrderCreated", source: "order-service") { }
        public OrderCreatedEvent(Guid orderId, decimal total, Guid customerId)
            : base(eventType: "OrderCreated", source: "order-service")
        {
            Payload = new OrderPayload
            {
                OrderId = orderId,
                Total = total,
                CustomerId = customerId
            };
        }
    }

    public class OrderPayload
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public Guid CustomerId { get; set; }
    }

}
