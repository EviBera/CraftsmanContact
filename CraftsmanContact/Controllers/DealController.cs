using System.Data;
using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CraftsmanContact.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DealController : ControllerBase
{
    private readonly ILogger<DealController> _logger;
    private readonly IDealRepository _dealRepository;

    public DealController(ILogger<DealController> logger, IDealRepository dealRepository)
    {
        _logger = logger;
        _dealRepository = dealRepository;
    }


    [HttpGet("GetByUser/{userId}")]
    public async Task<ActionResult<List<Deal>>> GetDealsByUserAsync([FromRoute]string userId)
    {
        try
        {
            var deals = await _dealRepository.GetDealsByUserAsync(userId);
            return Ok(deals);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting deals for user {userId}.");

            if (e is RowNotInTableException)
            {
                return BadRequest($"Invalid user Id.");
            }
            
            return NotFound($"Error getting deals, {e.Message}");
        }
    }

    [HttpGet("GetById/{dealId}")]
    public async Task<ActionResult<Deal>> GetDealByIdAsync([FromRoute]int dealId)
    {
        try
        {
            var deal = await _dealRepository.GetDealById(dealId);
            if (deal == null)
            {
                return NotFound("Deal does not exist.");
            }
            return Ok(deal);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting deal by Id {dealId}.");
            return NotFound($"Error getting deal, {e.Message}");
        }
    }

    [HttpPost("Post")]
    public async Task<ActionResult> AddNewDealAsync([FromBody] Deal deal)
    {
        try
        {
            await _dealRepository.CreateDealAsync(deal);
            return Ok("New deal is created.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error registering deal.");
            return BadRequest($"Error registering deal, {e.Message}");
        }
    }

    [HttpPatch("Update/Accept/{dealId}")]
    public async Task<ActionResult> AcceptDealAsync([FromRoute]int dealId)
    {
        try
        {
            await _dealRepository.SetDealToAcceptedAsync(dealId);
            return Ok("Deal accepted.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error accepting the deal offer id {dealId}");
            if (e is RowNotInTableException)
            {
                return BadRequest("Invalid parameters.");
            }
            return StatusCode(500, "Error accepting the deal.");
        }
    }

    [HttpPatch("Update/Close/{dealId}/{userId}")]
    public async Task<ActionResult> CloseDealAsync([FromRoute] int dealId, [FromRoute] string userId)
    {
        try
        {
            await _dealRepository.SetDealClosedAsync(dealId, userId);
            return Ok("Deal closed.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error closing the deal id: {dealId} by user {userId}");
            if (e is ArgumentException)
            {
                return BadRequest("Invalid parameters.");
            }

            return StatusCode(500, "Something went wrong.");
        }
    }
}