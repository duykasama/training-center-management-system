using FAMS.V0.Services.AuthenticationService.Dtos;
using FAMS.V0.Services.AuthenticationService.Services;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Domain.Dtos;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Exceptions;
using FAMS.V0.Shared.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.AuthenticationService.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRepository<User> _userRepository;
    private readonly JwtService _jwtService;

    public AuthenticationController(
        IRepository<User> userRepository,
        IPublishEndpoint publishEndpoint,
        IConfiguration configuration
    )
    {
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
        _jwtService = new JwtService(configuration);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(DtoUserRegister userRegister)
    {
        try
        {
            var user = new User()
            {
                Email = userRegister.Email,
                Name = userRegister.Name,
                CreatedDate = DateTimeOffset.Now,
                Role = Role.Trainee
            };
            await _userRepository.CreateAsync(user);

            var createdUser = await _userRepository.GetAsync(filter => filter.Email == userRegister.Email);

            if (createdUser is null)
            {
                throw new Exception("An error occurred");
            }

            var tokens = _jwtService.GenerateToken(createdUser);
            Response.Cookies.Append("refreshToken", tokens.refreshToken);

            return Ok(tokens.accessToken);
        }
        catch (EntityAlreadyExistsException)
        {
            return BadRequest("User already exists");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(DtoUserLogin userLogin)
    {
        try
        {
            var retrievedUser = await _userRepository.GetAsync(filter => filter.Email == userLogin.Email);
            if (retrievedUser is null)
            {
                throw new EntityDoesNotExistException();
            }

            // BCrypt.Net.BCrypt.Verify(userLogin.Password, retrievedUser.Email);
            
            var tokens = _jwtService.GenerateToken(retrievedUser);
            Response.Cookies.Append("refreshToken", tokens.refreshToken);
            
            return Ok(tokens.accessToken);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        
        return Ok();
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok();
    }
}