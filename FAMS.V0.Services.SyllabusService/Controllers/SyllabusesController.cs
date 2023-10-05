using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.SyllabusService.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class SyllabusesController : Controller
{
    [HttpGet]
    public IActionResult GetAllSyllabusesAsync()
    {
        return Ok();
    }
}