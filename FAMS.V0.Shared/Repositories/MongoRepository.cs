using System.Linq.Expressions;
using FAMS.V0.Shared.Exceptions;
using FAMS.V0.Shared.Interfaces;
using MongoDB.Driver;

namespace FAMS.V0.Shared.Repositories;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _mongoCollection;
    private readonly FilterDefinitionBuilder<T> _filter = Builders<T>.Filter;

    public MongoRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
    }
    
    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await _mongoCollection.Find(_filter.Empty).ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await _mongoCollection.Find(filter).ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetPerPageAsync(int pageSize, int offset)
    {
        return await _mongoCollection.Find(_filter.Empty).Skip((offset - 1) * pageSize).Limit(pageSize).ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetPerPageAsync(int pageSize, int offset, Expression<Func<T, bool>> filter)
    {
        return await _mongoCollection.Find(filter).Skip((offset - 1) * pageSize).Limit(pageSize).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var filter = _filter.Eq(u => u.Id, id);
        return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        var t = await GetByIdAsync(entity.Id);
        if (t is not null)
        {
            throw new EntityAlreadyExistsException();
        }

        await _mongoCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        var t = await GetByIdAsync(entity.Id);
        if (t is null)
        {
            throw new EntityDoesNotExistException();
        }

        var filter = _filter.Eq(x => x.Id, entity.Id);
        await _mongoCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var t = await GetByIdAsync(id);
        if (t is null)
        {
            throw new EntityDoesNotExistException();
        }
    
        var filter = _filter.Eq(x => x.Id, id);
        await _mongoCollection.DeleteOneAsync(filter);
    }
}