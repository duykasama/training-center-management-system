using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Interfaces;
using FAMS.V0.Shared.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Gender = FAMS.V0.Shared.Constants.Gender;


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
}