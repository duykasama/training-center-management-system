using FAMS.V0.Services.UserService.Dtos;
using FAMS.V0.Shared.Domain.Entities;

namespace FAMS.V0.Services.UserService.Extensions;

public static class UserExtensions
{
    public static DtoUser ToUserDto(this User userEntity)
    {
        return new DtoUser()
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Email = userEntity.Email,
            Phone = userEntity.Phone,
            Dob = userEntity.Dob,
            Gender = userEntity.Gender,
            Role = userEntity.Role,
            Status = userEntity.Status,
            CreatedBy = userEntity.CreatedBy,
        };
    }

    public static User ToEntity(this DtoUser dtoUser)
    {
        return new User()
        {
            Name = dtoUser.Name,
            Email = dtoUser.Name,
            Phone = dtoUser.Phone,
            Dob = dtoUser.Dob,
            Gender = dtoUser.Gender,
            Role = dtoUser.Role,
            Status = dtoUser.Status,
            CreatedBy = dtoUser.CreatedBy,
        };
    }

    public static User ToEntity(this DtoUserUpdate userDto, User user)
    {
        user.Name = userDto.Name;
        user.Email = userDto.Email;
        user.Phone = userDto.Phone;
        user.Dob = userDto.Dob;
        user.Gender = userDto.Gender;
        user.Role = userDto.Role;
        user.Status = userDto.Status;
        user.ModifiedDate = DateTimeOffset.Now;
        return user;
    }

    public static User ToEntity(this DtoUserCreate userDto)
    {
        return new User()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Phone = userDto.Phone,
            Dob = userDto.Dob,
            Gender = userDto.Gender,
            Role = userDto.Role,
            Status = userDto.Status,
            CreatedBy = userDto.CreatedBy ?? Guid.Empty,
            ModifiedBy = userDto.CreatedBy ?? Guid.Empty,
            CreatedDate = DateTimeOffset.Now,
            ModifiedDate = DateTimeOffset.Now
        };
    }
}