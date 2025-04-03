using Order.Domain.Abstractions;

namespace Order.Infrastructure.MessageBroker
{
    public interface IRabbitMQService
    {
        Task PublishMessageAsync<T>(T message) where T : BaseEvent;
    }
}
