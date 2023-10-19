using FAMS.V0.Shared.Constants;

namespace FAMS.V0.Services.UserService.Dtos;

public class DtoUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; }
    public DateTime Dob { get; set; }
    public string Gender { get; set; }
    public Role Role { get; set; }
    public string Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedDate { get; set; } = DateTimeOffset.Now;
}