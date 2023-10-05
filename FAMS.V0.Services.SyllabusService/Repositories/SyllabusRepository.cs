using FAMS.V0.Services.SyllabusService.Entities;
using FAMS.V0.Shared.DbClient;
using FAMS.V0.Shared.Enums;
using FAMS.V0.Shared.Interfaces;
using MongoDB.Driver;

namespace FAMS.V0.Services.SyllabusService.Repositories;

public class SyllabusRepository : IRepository<Syllabus>
{
    private readonly IMongoCollection<Syllabus> _mongoCollection;
    private readonly FilterDefinitionBuilder<Syllabus> _filter = Builders<Syllabus>.Filter;
    
    public SyllabusRepository(MongoDbClient mongoDbClient)
    {
        var database = mongoDbClient.GetDatabase(Database.FAMS_DB);
        _mongoCollection = database.GetCollection<Syllabus>(Collection.Syllabus);
    }
    
    public Task<IReadOnlyCollection<Syllabus>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Syllabus>> GetPerPageAsync(int pageSize, int offset)
    {
        throw new NotImplementedException();
    }

    public Task<Syllabus?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateUserAsync(Syllabus entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Syllabus entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}