using System.Runtime.InteropServices.JavaScript;

namespace FAMS.V0.Services.ProgramService.Dtos;

public class ProgramDto
{
    public string ProgramCode { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartTime { get; set;} = DateTimeOffset.Now;
    public TimeSpan Duration { get; set; }
    public string Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedDate { get; set; } = DateTimeOffset.Now;
}
