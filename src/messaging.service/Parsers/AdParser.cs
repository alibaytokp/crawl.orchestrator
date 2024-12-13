using messaging.service.Models.Ad;

namespace messaging.service.Parsers;

public class AdParser : IAdParser
{
    public AdMessage Parse(string adContent)
    {
        
        return new AdMessage
        {
            HtmlContent = adContent,
            CreatedAt = DateTime.UtcNow
        };
    }
}
