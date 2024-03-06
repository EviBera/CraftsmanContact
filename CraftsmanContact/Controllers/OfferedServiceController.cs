using System.ComponentModel.DataAnnotations;
using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CraftsmanContact.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfferedServiceController : ControllerBase
{
    private readonly ILogger<OfferedServiceController> _logger;
    private readonly IOfferedServiceRepository _offeredServiceRepository;

    public OfferedServiceController(ILogger<OfferedServiceController> logger, IOfferedServiceRepository osr)
    {
        _logger = logger;
        _offeredServiceRepository = osr;
    }
    
    // GET
    [HttpGet("Get")]
    public async Task<ActionResult<List<OfferedService>>> GetAllAsync()
    {
        try
        {
            var offeredServices = await _offeredServiceRepository.GetAllAsync();
            return Ok(offeredServices);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting offered services.");
            return NotFound($"Error getting offered services, {e.Message}");
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<OfferedService>> GetByIdAsync([Required, FromRoute]int id)
    {
        try
        {
            var offeredService = await _offeredServiceRepository.GetByIdAsync(id);
            if (offeredService != null)
            {
                return Ok(offeredService);
            }
            else
            {
                return NotFound($"The searched offered service does not exist.");
            }
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting offered service id: " + id);
            return StatusCode(500, $"Error getting offered service, {e.Message}");
        }
    }
    
    //POST
    [HttpPost("Post")]
    public async Task<ActionResult> RegisterNewOfferedServiceAsync([FromBody]OfferedService service)
    {
        try
        {
            await _offeredServiceRepository.RegisterAsync(service);
            return Ok("New service registered successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error registering the offered service.");
            return BadRequest($"Error registering the offered service, {e.Message}");
        }
    }
    
    //UPDATE
    [HttpPatch("Update/{id}")]
    public async Task<ActionResult> UpdateOfferedServiceAsync(string? newName, string? newDescription, [Required, FromRoute]int id)
    {
        try
        {
            await _offeredServiceRepository.UpdateAsync(id, newName, newDescription);
            return Ok("Service updated successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating the offered service id: " + id );
            return BadRequest($"Error updating the offered service, {e.Message}");
        }
    }
    
    //DELETE
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteOfferedService([Required, FromRoute] int id)
    {
        try
        {
            await _offeredServiceRepository.DeleteAsync(id);
            return Ok("Service deleted successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the offered service id: " + id);
            return BadRequest($"Error deleting the offered service, {e.Message}");
        }
    }
}