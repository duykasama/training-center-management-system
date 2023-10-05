using FAMS.V0.Services.UserService.Dtos;
using FAMS.V0.Services.UserService.Entities;
using FAMS.V0.Services.UserService.Mappers;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Exceptions;
using FAMS.V0.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.UserService.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger = new Logger<UsersController>(new LoggerFactory());
    private const string ServerError = "Server error";
    private readonly IRepository<User> _userRepository;

    public UsersController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users.Select(UserMapper.EntityToUserDto));
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
            return Ok(users.Select(UserMapper.EntityToUserDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto userDto)
    {
        try
        {
            var userEntity = UserMapper.CreateUserDtoToEntity(userDto);
            
            await _userRepository.CreateUserAsync(userEntity);
            return CreatedAtAction(nameof(CreateUser), new {id = userEntity.Id}, userEntity);
        }
        catch (UserAlreadyExistsException e)
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
                Status = Status.Active,
                CreatedBy = Guid.Empty,
                ModifiedBy = Guid.Empty,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now
            };
            await _userRepository.CreateUserAsync(superAdmin);
            return CreatedAtAction(nameof(CreateSuperAdmin), new { id = superAdmin.Id }, superAdmin);
        }
        catch (UserAlreadyExistsException e)
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
    public async Task<IActionResult> UpdateUserAsync(Guid id, UpdateUserDto userDto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                throw new UserDoesNotExistException();
            }

            user = UserMapper.UpdateUserDtoToEntity(user, userDto);
            await _userRepository.UpdateAsync(user);
            return NoContent();
        }
        catch (UserDoesNotExistException e)
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
            return NoContent();
        }
        catch (UserDoesNotExistException e)
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