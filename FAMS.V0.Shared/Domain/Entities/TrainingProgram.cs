using FAMS.V0.Shared.Interfaces;

namespace FAMS.V0.Shared.Domain.Entities;

public class TrainingProgram : IEntity
{
    public Guid Id { get; set; }
    public string TrainingProgramCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTimeOffset StartTime { get; set;} 
    public TimeSpan Duration { get; set; }
    public string Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; } 
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}