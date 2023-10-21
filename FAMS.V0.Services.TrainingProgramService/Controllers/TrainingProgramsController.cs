using FAMS.V0.Services.ProgramService.Dtos;
using FAMS.V0.Services.ProgramService.Mapper;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Events.TrainingProgramEvents;
using FAMS.V0.Shared.Exceptions;
using FAMS.V0.Shared.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.ProgramService.Controllers;

[ApiController]
[Route("/api/v0/training-program")]
public class TrainingProgramsController : Controller
{
    private readonly ILogger<TrainingProgramsController> _logger =
        new Logger<TrainingProgramsController>(new LoggerFactory());

    private const string ServerError = "Server error";
    private readonly IRepository<TrainingProgram> _programRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public TrainingProgramsController(IRepository<TrainingProgram> programRepository, IPublishEndpoint publishEndpoint)
    {
        _programRepository = programRepository;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllTrainingPrograms()
    {
        try
        {
            var programs = await _programRepository.GetAllAsync();
            return Ok(programs.Select(TrainingProgramMapper.EntityToProgramDto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Problem(ServerError);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTrainingPrograms(int pageSize, int offset)
    {
        try
        {
            var programs = await _programRepository.GetPerPageAsync(pageSize, offset);
            return Ok(programs.Select(TrainingProgramMapper.EntityToProgramDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem(ServerError);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTrainingProgramById(Guid id)
    {
        try
        {
            var program = await _programRepository.GetByIdAsync(id);
            if (program is null)
            {
                throw new EntityDoesNotExistException();
            }

            return Ok(TrainingProgramMapper.EntityToProgramDto(program));
        }
        catch (EntityDoesNotExistException e)
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
    public async Task<IActionResult> CreateTrainingProgram(CreateTrainingProgramDto trainingProgramDto){
        try
        {
            var trainingProgramEntity = TrainingProgramMapper.CreateTrainingProgramDtoToEntity(trainingProgramDto);
            await _programRepository.CreateAsync(trainingProgramEntity);
            await _publishEndpoint.Publish(new EventProgramCreated(trainingProgramEntity));
            return CreatedAtAction(nameof(CreateTrainingProgram), new { id = trainingProgramEntity.Id },
                trainingProgramEntity);
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
    public async Task<IActionResult> UpdateTrainingProgram(Guid id, TrainingProgramDto programDto)
    {
        try
        {
            var program = await _programRepository.GetByIdAsync(id);
            if (program is null)
            {
                throw new EntityDoesNotExistException();
            }

            program = TrainingProgramMapper.UpdateTrainingProgramDtoToEntity(program, programDto);
            await _programRepository.UpdateAsync(program);
            await _publishEndpoint.Publish(new EventProgramUpdated(program));
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
    public async Task<IActionResult> DeleteTrainingProgram(Guid id)
    {

        try
        {
            await _programRepository.DeleteAsync(id);

            await _publishEndpoint.Publish(new EventProgramDeleted(id));

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