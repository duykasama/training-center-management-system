using System.Linq.Expressions;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Interfaces;

namespace FAMS.V0.Shared.Repositories;

public class TrainingProgramRepository : IRepository<TrainingProgram>
{
    public Task<IReadOnlyCollection<TrainingProgram>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<TrainingProgram>> GetAllAsync(Expression<Func<TrainingProgram, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<TrainingProgram>> GetPerPageAsync(int pageSize, int offset)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<TrainingProgram>> GetPerPageAsync(int pageSize, int offset, Expression<Func<TrainingProgram, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task<TrainingProgram?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TrainingProgram?> GetAsync(Expression<Func<TrainingProgram, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(TrainingProgram entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TrainingProgram entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}