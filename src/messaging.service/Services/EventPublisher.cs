using MassTransit;
using messaging.service.Interfaces;
using Microsoft.Extensions.Logging;

namespace messaging.service.Services;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<EventPublisher> _logger;

    public EventPublisher(IPublishEndpoint publishEndpoint, ILogger<EventPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishEventAsync<TEvent>(TEvent eventMessage) where TEvent : class
    {
        if (eventMessage == null)
        {
            _logger.LogWarning("Attempted to publish a null event.");
            return;
        }

        _logger.LogInformation("Publishing event of type {EventType}", typeof(TEvent).Name);

        await _publishEndpoint.Publish(eventMessage);
    }
}
