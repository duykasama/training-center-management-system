using System.ComponentModel.DataAnnotations.Schema;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Interfaces;

namespace FAMS.V0.Shared.Domain.Entities;

public class User : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; }
    public DateTime Dob { get; set; }
    public string Gender { get; set; }
    public Role Role { get; set; }
    public string Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
    public Guid Permission { get; set; }
    public IEnumerable<Guid> Class { get; set; } = new List<Guid>();
}