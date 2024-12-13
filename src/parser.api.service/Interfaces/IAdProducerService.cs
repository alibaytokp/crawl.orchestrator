using parser.api.service.Models;

namespace parser.api.service.Interfaces;

public interface IAdProducerService
{
    Task PublishAdMessageAsync(AdMessage adMessage);
}