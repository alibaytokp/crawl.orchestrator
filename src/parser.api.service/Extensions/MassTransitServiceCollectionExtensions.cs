using MassTransit;

namespace parser.api.service.Extensions;

public static class MassTransitServiceCollectionExtensions
{

    public static IServiceCollection AddMassTransitWithEnvironment(this IServiceCollection services, IConfiguration configuration,bool isProd)
    {
        services.AddMassTransit(x =>
        {
            if (!isProd)
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.RabbitMqHost());
                });
            }
            else
            {
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(configuration.AzureServiceBusConnectionString());

                    cfg.ConfigureEndpoints(context);
                });
            }
        });


        return services;
    }
}