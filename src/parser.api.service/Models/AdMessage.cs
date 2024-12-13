namespace parser.api.service.Models;
public class AdMessage
{
    public string Portal { get; set; }
    public string HtmlContent { get; set; }
    public string? OperationType { get; set; } // e.g., "NewAd", "UpdateAd", "UrlCheck"
    public string ProviderCode { get; set; }
    public string Url { get; set; }

    public DateTime? CreatedAt { get; set; }
}