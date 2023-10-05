using FAMS.V0.Shared.Enums;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FAMS.V0.Shared.DbClient;

public class MongoDbClient
{
    private readonly IMongoClient _mongoClient;

    public MongoDbClient(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(Connection.MongoDbConnection);
        _mongoClient = new MongoClient(connectionString);
    }
    
    public IMongoDatabase GetDatabase(string databaseName)
    {
        return _mongoClient.GetDatabase(databaseName);
    }
}