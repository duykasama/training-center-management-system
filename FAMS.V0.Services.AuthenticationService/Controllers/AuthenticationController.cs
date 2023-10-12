using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.AuthenticationService.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class AuthenticationController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {

        return Ok();
    }
}