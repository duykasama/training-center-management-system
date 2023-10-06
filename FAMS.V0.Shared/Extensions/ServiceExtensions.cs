using System.Reflection;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Interfaces;
using FAMS.V0.Shared.Repositories;
using FAMS.V0.Shared.Settings;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


namespace FAMS.V0.Shared.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            return new MongoClient(configuration.GetConnectionString(Connection.MongoDbConnection)).GetDatabase(
                Database.FAMS_DB);
        });

        return services;
    }

    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            return new MongoRepository<T>(database, collectionName);
        });

        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services, string serviceName)
    {
        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(Assembly.GetEntryAssembly());
            
            configure.UsingRabbitMq((ctx, configurator) =>
            {
                var configuration = ctx.GetService<IConfiguration>();
                var rabbitMqHost = configuration.GetSection(nameof(RabbitMqSettings)).GetSection(nameof(RabbitMqSettings.Host));
                configurator.Host(rabbitMqHost.Value);
                configurator.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter(serviceName, false));
            });
        });

        services.AddMassTransitHostedService();

        return services;
    }
}