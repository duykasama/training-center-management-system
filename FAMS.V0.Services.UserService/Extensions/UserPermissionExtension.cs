using FAMS.V0.Services.UserService.Dtos;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Domain.Entities;

namespace FAMS.V0.Services.UserService.Extensions;

public static class UserPermissionExtension
{
    public static UserPermission ToEntity(DtoPermissionsUpdateRequest permissionDto)
    {
        return new UserPermission
        {
            Id = default,
            Role = (Role)0,
            Syllabus = null,
            TrainingProgram = null,
            Class = null,
            LearningMaterial = null,
            UserManagement = null
        };
    }
}