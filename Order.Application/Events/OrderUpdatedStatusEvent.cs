using Order.Domain.Abstractions;
using Order.Domain.Enum;

namespace Order.Application.Events
{
    public class OrderUpdatedStatusEvent : BaseEvent
    {
        public OrderUpdatedStatusPayload Payload { get; set; }

        public OrderUpdatedStatusEvent() : base(eventType: "UpdatedStatusOrder", source: "order-service") { }

        public OrderUpdatedStatusEvent(Guid Id, OrderStatus status) : base(eventType: "UpdatedStatusOrder", source: "order-service")
        {
            Payload.Id = Id;
            Payload.Status = status;
        }
    }

    public class OrderUpdatedStatusPayload
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
