namespace messaging.service.Parsers;

public static class AdParserFactory
{
    public static IAdParser GetParser(string type)
    {
        
        return type.ToLower() switch
        {
            "html" => new AdParser(),
            _ => throw new NotSupportedException($"Parser for portal '{type}' is not supported.")
        };
    }
}