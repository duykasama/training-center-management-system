using FAMS.V0.Services.UserService.Entities;
using FAMS.V0.Shared.Repositories;
using MongoDB.Driver;

namespace FAMS.V0.Services.UserService.Repositories;

public class UserRepository : MongoRepository<User>
{
    public UserRepository(IMongoDatabase mongoDatabase, string collectionName) : base(mongoDatabase, collectionName)
    {
    }
}