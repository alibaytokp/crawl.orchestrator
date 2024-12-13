namespace messaging.service.Interfaces
{
    public interface IAdMessage
    {
        DateTime CreatedAt { get; set; }
        string HtmlContent { get; set; }
        string OperationType { get; set; }
        string Portal { get; set; }
        string ProviderCode { get; set; }
        string Url { get; set; }
    }
}