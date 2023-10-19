using System.ComponentModel.DataAnnotations.Schema;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Interfaces;

namespace FAMS.V0.Shared.Domain.Entities;

public class UserPermission : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Role Role { get; set; }
    public IEnumerable<string> Syllabus { get; set; } = new List<string>();
    public IEnumerable<string> TrainingProgram { get; set; } = new List<string>();
    public IEnumerable<string> Class { get; set; } = new List<string>();
    public IEnumerable<string> LearningMaterial { get; set; } = new List<string>();
    public IEnumerable<string> UserManagement { get; set; } = new List<string>();
}