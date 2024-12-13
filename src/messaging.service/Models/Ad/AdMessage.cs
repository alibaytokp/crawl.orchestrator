using messaging.service.Interfaces;

namespace messaging.service.Models.Ad;

public class AdMessage : IAdMessage
{
    public string Portal { get; set; }
    public string HtmlContent { get; set; }
    public string OperationType { get; set; }
    public string ProviderCode { get; set; }
    public string Url { get; set; }

    public DateTime CreatedAt { get; set; }
}