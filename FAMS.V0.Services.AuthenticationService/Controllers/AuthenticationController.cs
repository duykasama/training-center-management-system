using System.Security.Authentication;
using FAMS.V0.Services.AuthenticationService.Dtos;
using FAMS.V0.Services.AuthenticationService.Services;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Domain.Dtos;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Exceptions;
using FAMS.V0.Shared.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
                Password = BCrypt.Net.BCrypt.HashPassword(userRegister.Password),
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

            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, retrievedUser.Password))
            {
                throw new InvalidCredentialException();
            }

            var tokens = _jwtService.GenerateToken(retrievedUser);
            Response.Cookies.Append("refreshToken", tokens.refreshToken);
            
            return Ok(tokens.accessToken);
        }
        catch (EntityDoesNotExistException)
        {
            return Unauthorized("User does not exist");
        }
        catch (InvalidCredentialException)
        {
            return Unauthorized("Invalid password");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("refreshToken");
        return NoContent();
    }

    [HttpPost]
    [Route("refresh")]
    public IActionResult RefreshToken()
    {
        try
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken is null)
            {
                return BadRequest("Refresh token does not exist");
            }

            var newAccessToken = _jwtService.RefreshToken(refreshToken);
            
            return Ok(newAccessToken);
        }
        catch (SecurityTokenExpiredException)
        {
            return Forbid("Token expired");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}