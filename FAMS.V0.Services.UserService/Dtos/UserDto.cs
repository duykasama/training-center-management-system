using FAMS.V0.Services.UserService.Entities;
using FAMS.V0.Shared.Enums;

namespace FAMS.V0.Services.UserService.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; }
    public DateTime Dob { get; set; }
    public Gender Gender { get; set; }
    public Role Role { get; set; }
    public Status Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedDate { get; set; } = DateTimeOffset.Now;
}