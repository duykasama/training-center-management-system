using FAMS.V0.Services.UserService.Dtos;
using FAMS.V0.Shared.Entities;

namespace FAMS.V0.Services.UserService.Mappers;

public static class UserMapper
{
    public static UserDto EntityToUserDto(User userEntity)
    {
        return new UserDto()
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

    public static User UserDtoToEntity(UserDto userDto)
    {
        return new User()
        {
            Name = userDto.Name,
            Email = userDto.Name,
            Phone = userDto.Phone,
            Dob = userDto.Dob,
            Gender = userDto.Gender,
            Role = userDto.Role,
            Status = userDto.Status,
            CreatedBy = userDto.CreatedBy,
        };
    }

    public static User UpdateUserDtoToEntity(User user, UpdateUserDto userDto)
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

    public static User CreateUserDtoToEntity(CreateUserDto userDto)
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
            CreatedBy = userDto.CreatedBy,
            ModifiedBy = userDto.CreatedBy,
            CreatedDate = DateTimeOffset.Now,
            ModifiedDate = DateTimeOffset.Now
        };
    }
}