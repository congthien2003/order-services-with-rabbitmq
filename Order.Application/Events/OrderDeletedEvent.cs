using Order.Domain.Abstractions;

namespace Order.Application.Events
{
    public class OrderDeletedEvent : BaseEvent
    {
        public OrderDeletedPayload Payload { get; set; }
        public OrderDeletedEvent() : base(eventType: "OrderDeleted", source: "order-service")
        {
        }

        public OrderDeletedEvent(Guid id) : base(eventType: "OrderDeleted", source: "order-service")
        {
            Payload.Id = id;
        }

    }

    public class OrderDeletedPayload
    {
        public Guid Id { get; set; }
    }
}
