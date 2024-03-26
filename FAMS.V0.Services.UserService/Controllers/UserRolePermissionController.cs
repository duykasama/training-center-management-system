using FAMS.V0.Services.UserService.Attribute;
using FAMS.V0.Services.UserService.Dtos;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.UserService.Controllers;

[ApiController]
[Route("api/v0/user-role-permission")]
public class UserRolePermissionController : Controller
{
    private readonly IRepository<UserPermission> _userPermissionRepository;

    public UserRolePermissionController(IRepository<UserPermission> userPermissionRepository)
    {
        _userPermissionRepository = userPermissionRepository;
    }
    
    /// <summary>
    /// Create permissions for 3 main roles (Super Admin, Class Admin and Trainer). This endpoint should only be called once.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> InitDefaultPermissions()
    {
        var fullAccess = new[]
        {
            Permission.Create,
            Permission.Read,
            Permission.Update,
            Permission.Delete,
            Permission.Import
        };
        
        var superAdminPermission = new UserPermission()
        {
            Role = Role.SuperAdmin,
            Syllabus = fullAccess,
            TrainingProgram = fullAccess,
            Class = fullAccess,
            LearningMaterial = fullAccess,
            UserManagement = fullAccess
        };
        
        var classAdminPermission = new UserPermission()
        {
            Role = Role.Admin,
            Syllabus = fullAccess,
            TrainingProgram = fullAccess,
            Class = fullAccess,
            LearningMaterial = fullAccess,
            UserManagement = ArraySegment<string>.Empty
        };
        
        var trainerPermission = new UserPermission()
        {
            Role = Role.Trainer,
            Syllabus = new[]
            {
                Permission.Create,
                Permission.Read,
                Permission.Update,
                Permission.Delete,
            },
            TrainingProgram = new[]
            {
                Permission.Read,
            },
            Class = new[]
            {
                Permission.Read,
            },
            LearningMaterial = new[]
            {
                Permission.Create,
                Permission.Read,
                Permission.Update,
                Permission.Delete,
                
            },
            UserManagement = ArraySegment<string>.Empty
        };

        await _userPermissionRepository.CreateAsync(superAdminPermission);
        await _userPermissionRepository.CreateAsync(classAdminPermission);
        await _userPermissionRepository.CreateAsync(trainerPermission);
        
        return CreatedAtAction(nameof(InitDefaultPermissions), new { }, "Default permissions created");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await _userPermissionRepository.GetAllAsync();
        return Ok(permissions);
    }

    [HttpPut]
    public async Task UpdateUserPermissions(DtoPermissionsUpdateRequest dtoPermissionsUpdateRequest)
    {

        await _userPermissionRepository.UpdateAsync(null!);
    }
}