using ExerciseTracker_DotNet8.Models;
using ExerciseTracker_DotNet8.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ExerciseTracker_DotNet8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExerciseLogController : ControllerBase
{
    private readonly IExerciseLogRepository exerciseRepository;

    public ExerciseLogController(IExerciseLogRepository exerciseRepository)
    {
        this.exerciseRepository = exerciseRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllLogs()
    {
        try
        {
            return Ok(await exerciseRepository.GetAllLogs());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExerciseLog>> GetLog(int id)
    {
        try
        {
            var result = await exerciseRepository.GetLog(id);
            if (result == null) return StatusCode(StatusCodes.Status500InternalServerError,
                "Could not find Exercise Log with Id requested");
            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from Database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseLog>> AddLog(ExerciseLog log)
    {
        try
        {
            if (log == null)
                return BadRequest();

            var createdLog = await exerciseRepository.AddLog(log);

            return CreatedAtAction(nameof(log),
                new { id = createdLog.Id }, createdLog);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new record");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ExerciseLog>> UpdatedEmployee(int id, ExerciseLog log)
    {
        try
        {
            if (id != log.Id)
                return BadRequest("Log ID mismatch");

            var logToUpdate = await exerciseRepository.GetLog(id);

            if (logToUpdate == null)
                return NotFound($"Log with id = {id} not found");

            return await exerciseRepository.UpdateLog(log);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ExerciseLog>> DeleteLog(int id)
    {
        try
        {
            var logToDelete = await exerciseRepository.GetLog(id);

            if (logToDelete == null)
            {
                return NotFound($"Log with Id = {id} not found");
            }

            return await exerciseRepository.DeleteLog(id);
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
}
