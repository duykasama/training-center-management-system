using FAMS.V0.Services.UserService.Entities;
using FAMS.V0.Shared.Enums;

namespace FAMS.V0.Services.UserService.Dtos;

public class UpdateUserDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; }
    public DateTime Dob { get; set; }
    public Gender Gender { get; set; }
    public Role Role { get; set; }
    public Status Status { get; set; }
}