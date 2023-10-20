using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.ProgramService.Controllers;
[Authorize]
[ApiController]
[Route("/api/v0/[controller]")]
public class TrainingProgramController : Controller
{
    
}