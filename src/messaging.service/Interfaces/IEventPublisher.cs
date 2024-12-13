namespace messaging.service.Interfaces;

public interface IEventPublisher
{
    Task PublishEventAsync<TEvent>(TEvent eventMessage) where TEvent : class;
}