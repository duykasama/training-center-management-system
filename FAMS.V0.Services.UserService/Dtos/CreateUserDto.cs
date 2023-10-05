using FAMS.V0.Services.UserService.Entities;
using FAMS.V0.Shared.Enums;

namespace FAMS.V0.Services.UserService.Dtos;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Dob { get; set; }
    public Gender Gender { get; set; }
    public Role Role { get; set; }
    public Status Status { get; set; }
    public Guid CreatedBy { get; set; }
}