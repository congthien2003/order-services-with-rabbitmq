using Order.Domain.Abstractions;

namespace Order.Application
{
    public interface IRabbitMQService
    {
        Task PublishMessageAsync<T>(T message) where T : BaseEvent;
    }
}
