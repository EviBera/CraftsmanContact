using System.ComponentModel.DataAnnotations;
using System.Data;
using CraftsmanContact.DTOs;
using CraftsmanContact.Mappers;
using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
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
    
    
    [HttpGet("Get")]
    public async Task<ActionResult<List<OfferedServiceDto>>> GetAllAsync()
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
    public async Task<ActionResult<OfferedServiceDto>> GetByIdAsync([Required, FromRoute]int id)
    {
        try
        {
            var offeredService = await _offeredServiceRepository.GetByIdAsync(id);
            return Ok(offeredService);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting offered service id: " + id);
            if (e is RowNotInTableException)
            {
                return NotFound($"The searched offered service does not exist.");
            }
            return StatusCode(500, $"Error getting offered service, {e.Message}");
        }
    }
    
    [HttpPost("Post")]
    public async Task<ActionResult<OfferedServiceDto>> RegisterNewOfferedServiceAsync([FromBody]CreateRequestOfferedServiceDto serviceDto)
    {
        try
        {
            var newService = await _offeredServiceRepository.RegisterAsync(serviceDto);
            return Ok(newService.ToOfferedServiceDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error registering the offered service.");
            return BadRequest($"Error registering the offered service, {e.Message}");
        }
    }
    
    [HttpPatch("Update/{id}")]
    public async Task<ActionResult> UpdateOfferedServiceAsync([Required, FromRoute]int id, [FromBody]UpdateRequestOfferedServiceDto serviceDto)
    {
        try
        {
            var service = await _offeredServiceRepository.UpdateAsync(id, serviceDto);
            return Ok(service);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating the offered service id: " + id );
            if (e is RowNotInTableException)
            {
                return BadRequest($"This offered service does not exist.");
            }

            return StatusCode(500, "Something went wrong");
        }
    }
    
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteOfferedServiceAsync([Required, FromRoute] int id)
    {
        try
        {
            await _offeredServiceRepository.DeleteAsync(id);
            return Ok("Service deleted successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the offered service id: " + id);
            if (e is RowNotInTableException)
            {
                return BadRequest("Invalid Id");
            }
            return StatusCode(500, $"Error deleting the offered service, {e.Message}");
        }
    }
}