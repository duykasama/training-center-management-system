using FAMS.V0.Shared.Constants;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FAMS.V0.Shared.MongoDb;

public class MongoDbClient
{
    private readonly IMongoClient _mongoClient;

    public MongoDbClient(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DbConnection.MongoDbConnection);
        _mongoClient = new MongoClient(connectionString);
    }
    
    public IMongoDatabase GetDatabase(string databaseName)
    {
        return _mongoClient.GetDatabase(databaseName);
    }
}