namespace FAMS.V0.Services.UserService.Dtos;

public class DtoPermissionsUpdateRequest
{
    public string[] SuperAdminPermissions { get; set; }
    public string[] ClassAdminPermission { get; set; }
    public string[] TrainerPermission { get; set; }
}