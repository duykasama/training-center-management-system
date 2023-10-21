using System.Runtime.InteropServices.JavaScript;

namespace FAMS.V0.Services.ProgramService.Dtos;

public class TrainingProgramDto
{
    public Guid Id { get; set; }
    public string TrainingProgramCode { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartTime { get; set;} 
    public TimeSpan Duration { get; set; }
    public string Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; } 
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedDate { get; set; } 
}
