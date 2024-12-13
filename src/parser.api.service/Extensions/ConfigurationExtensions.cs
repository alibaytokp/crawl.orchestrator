namespace parser.api.service.Extensions;
public static class ConfigurationExtensions
{
    public static string AzureServiceBusConnectionString(this IConfiguration configuration) => configuration["azure:servicebus:connectionstring"];

    public static string RabbitMqHost(this IConfiguration configuration) => configuration["rabbitmq:host"];
}