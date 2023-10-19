using FAMS.V0.Services.UserService.Dtos;
using FAMS.V0.Services.UserService.Extensions;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Events.UserEvents;
using FAMS.V0.Shared.Exceptions;
using FAMS.V0.Shared.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.UserService.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0/[controller]")]
public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private const string ServerError = "Server error";
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserPermission> _userPermissionRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public UsersController(
        IRepository<User> userRepository,
        IRepository<UserPermission> userPermissionRepository,
        IPublishEndpoint publishEndpoint,
        ILogger<UsersController> logger
    )
    {
        _userRepository = userRepository;
        _userPermissionRepository = userPermissionRepository;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users.Select(UserExtensions.ToUserDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers(int pageSize, int offset)
    {
        try
        {
            var users = await _userRepository.GetPerPageAsync(pageSize, offset);
            return Ok(users.Select(UserExtensions.ToUserDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(DtoUserCreate userDto)
    {
        try
        {
            var userEntity = userDto.ToEntity();
            var retrievedUser = await _userRepository.GetAsync(e => e.Email == userEntity.Email);
            if (retrievedUser is not null)
            {
                throw new EntityAlreadyExistsException();
            }

            var trainerPermission = await _userPermissionRepository.GetAsync(p => p.Role == Role.Trainer);
            if (trainerPermission is null)
            {
                throw new MissingSystemPermissionsException();
            }

            userEntity.Permission = trainerPermission.Id;
            await _userRepository.CreateAsync(userEntity);
            await _publishEndpoint.Publish(new EventUserCreated(userEntity));
            return CreatedAtAction(nameof(CreateUser), new { id = userEntity.Id }, userEntity);
        }
        catch (EntityAlreadyExistsException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
        catch (MissingSystemPermissionsException e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
    }

    [HttpPost]
    [Route("create-super-admin")]
    public async Task<IActionResult> CreateSuperAdmin()
    {
        try
        {
            var superAdmin = new User()
            {
                Name = "Super Admin",
                Email = "superadmin@fams.com",
                Phone = "0788353099",
                Dob = DateTime.Now,
                Gender = Gender.Male,
                Role = Role.SuperAdmin,
                Status = UserStatus.Active,
                CreatedBy = Guid.Empty,
                ModifiedBy = Guid.Empty,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now
            };
            await _userRepository.CreateAsync(superAdmin);
            return CreatedAtAction(nameof(CreateSuperAdmin), new { id = superAdmin.Id }, superAdmin);
        }
        catch (EntityAlreadyExistsException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync(Guid id, DtoUserUpdate userDto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                throw new EntityDoesNotExistException();
            }

            user = userDto.ToEntity(user);
            await _userRepository.UpdateAsync(user);

            await _publishEndpoint.Publish(new EventUserUpdated(user));
            
            return NoContent();
        }
        catch (EntityDoesNotExistException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        try
        {
            await _userRepository.DeleteAsync(id);

            await _publishEndpoint.Publish(new EventUserDeleted(id));
            
            return NoContent();
        }
        catch (EntityDoesNotExistException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
    }
}