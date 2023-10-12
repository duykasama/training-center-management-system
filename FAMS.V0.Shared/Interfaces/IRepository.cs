using System.Linq.Expressions;

namespace FAMS.V0.Shared.Interfaces;

public interface IRepository<T> where T : IEntity
{
    public Task<IReadOnlyCollection<T>> GetAllAsync();
    public Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    public Task<IReadOnlyCollection<T>> GetPerPageAsync(int pageSize, int offset);
    public Task<IReadOnlyCollection<T>> GetPerPageAsync(int pageSize, int offset, Expression<Func<T, bool>> filter);
    public Task<T?> GetByIdAsync(Guid id);
    public Task<T?> GetAsync(Expression<Func<T, bool>> filter);
    public Task CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(Guid id);
}