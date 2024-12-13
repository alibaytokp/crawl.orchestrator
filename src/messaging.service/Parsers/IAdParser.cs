using messaging.service.Models.Ad;

namespace messaging.service.Parsers;
public interface IAdParser
{
    AdMessage Parse(string adContent);
}