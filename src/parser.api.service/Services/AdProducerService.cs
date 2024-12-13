using messaging.service.Interfaces;
using parser.api.service.Interfaces;
using parser.api.service.Models;


namespace parser.api.service.Services;

public class AdProducerService : IAdProducerService
{
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<AdProducerService> _logger;

    public AdProducerService(IEventPublisher eventPublisher, ILogger<AdProducerService> logger)
    {
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task PublishAdMessageAsync(AdMessage adMessage)
    {
        if (adMessage == null)
        {
            _logger.LogWarning("Attempted to publish a null AdMessage.");
            return;
        }
        if (string.IsNullOrEmpty(adMessage.HtmlContent))
        {
            _logger.LogWarning("Attempted to publish a null Content.");
            return;
        }
        if (string.IsNullOrEmpty( adMessage.ProviderCode))
        {
            _logger.LogWarning("Attempted to publish a null Provider Code.");
            return;
        }

        _logger.LogInformation("Publishing AdMessage: {Content} at {CreatedAt}", adMessage.HtmlContent, adMessage.CreatedAt);

        await _eventPublisher.PublishEventAsync(adMessage);

        _logger.LogInformation("Publishing AdMessage has been sent to parser service[{Content}], SentAt[{CreatedAt}]", adMessage.HtmlContent, adMessage.CreatedAt);
    }
}
