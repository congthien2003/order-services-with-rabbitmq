namespace Order.Domain.Abstractions
{
    public abstract class BaseEvent
    {
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        public string CorrelationId { get; set; }  // Liên kết với request lớn hơn
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string EventType { get; set; }
        public string Version { get; set; } = "1.0";
        public string Source { get; set; }

        protected BaseEvent(string eventType, string source)
        {
            EventType = eventType;
            Source = source;
        }
    }

}
