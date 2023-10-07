using FAMS.V0.Services.SyllabusService.Dtos;
using FAMS.V0.Services.SyllabusService.Entities;
using FAMS.V0.Services.SyllabusService.Mapper;
using FAMS.V0.Shared.Events.SyllabusEvents;
using FAMS.V0.Shared.Exceptions;
using FAMS.V0.Shared.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.V0.Services.SyllabusService.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class SyllabusesController : Controller
{
    private const string ServerError = "Server error";
    private readonly IRepository<Syllabus> _syllabusRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public SyllabusesController(IRepository<Syllabus> syllabusRepository, IPublishEndpoint publishEndpoint)
    {
        _syllabusRepository = syllabusRepository;
        _publishEndpoint = publishEndpoint;
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllSyllabuses()
    {
        try
        {
            var syllabuses = await _syllabusRepository.GetAllAsync();
            return Ok(syllabuses);
        }
        catch
        {
            return Problem(ServerError);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSyllabusPerPage(int pageSize, int offset)
    {
        try
        {
            var syllabuses = await _syllabusRepository.GetPerPageAsync(pageSize, offset);
            return Ok(syllabuses);
        }
        catch
        {
            return Problem(ServerError);
        }
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetSyllabusById(Guid id)
    {
        try
        {
            var syllabus = await _syllabusRepository.GetByIdAsync(id);
            return Ok(syllabus);
        }
        catch (EntityDoesNotExistException e)
        {
            return BadRequest(e.Message);
        }
        catch
        {
            return Problem(ServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSyllabus(DtoSyllabusCreate dto)
    {
        try
        {
            var syllabus = SyllabusMapper.DtoSyllabusCreateToEntity(dto);

            await _syllabusRepository.CreateAsync(syllabus);
            
            await _publishEndpoint.Publish(new EventSyllabusCreated(syllabus));

            return CreatedAtAction(nameof(CreateSyllabus), new {id = syllabus.Id}, syllabus);
        }
        catch (EntityAlreadyExistsException e)
        {
            return BadRequest(e.Message);
        }
        catch
        {
            return Problem(ServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSyllabus(Guid id, DtoSyllabusUpdate dto)
    {
        try
        {
            var syllabus = SyllabusMapper.DtoSyllabusUpdateToEntity(dto);

            await _syllabusRepository.UpdateAsync(syllabus);
            
            await _publishEndpoint.Publish(new EventSyllabusUpdated(syllabus));

            return NoContent();
        }
        catch (EntityDoesNotExistException e)
        {
            return BadRequest(e.Message);
        }
        catch
        {
            return Problem(ServerError);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSyllabus(Guid id)
    {
        try
        {
            await _syllabusRepository.DeleteAsync(id);

            await _publishEndpoint.Publish(new EventSyllabusDeleted(id));

            return NoContent();
        }
        catch (EntityDoesNotExistException e)
        {
            return BadRequest(e.Message);
        }
        catch
        {
            return Problem(ServerError);
        }
    }
}