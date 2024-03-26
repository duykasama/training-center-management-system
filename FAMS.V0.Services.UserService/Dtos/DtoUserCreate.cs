using System.Text.Json.Serialization;
using FAMS.V0.Shared.Constants;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FAMS.V0.Services.UserService.Dtos;

public class DtoUserCreate
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Dob { get; set; }
    public string Gender { get; set; }
    public Role Role { get; set; }
    public string Status { get; set; }
    public Guid? CreatedBy { get; set; }
}