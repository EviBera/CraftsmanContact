using System.Data;
using CraftsmanContact.DTOs.Deal;
using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CraftsmanContact.Controllers;

[Route("api/deal")]
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


    [HttpGet("byuser/{userId}")]
    public async Task<ActionResult<List<DealDto>>> GetDealsByUserAsync([FromRoute]string userId)
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

    [HttpGet("byid/{dealId}")]
    public async Task<ActionResult<DealDto>> GetDealByIdAsync([FromRoute]int dealId)
    {
        try
        {
            var deal = await _dealRepository.GetDealByIdAsync(dealId);
            return Ok(deal);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting deal by Id {dealId}.");
            if (e is RowNotInTableException)
            {
                return BadRequest("Invalid Id");
            }
            return StatusCode(500, $"Error getting deal.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddNewDealAsync([FromBody] CreateDealRequestDto dealDto)
    {
        try
        {
            await _dealRepository.CreateDealAsync(dealDto);
            return Created();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error registering deal.");
            return BadRequest($"Error registering deal, {e.Message}");
        }
    }

    [HttpPatch("accept/{craftsmanId}/{dealId}")]
    public async Task<ActionResult> AcceptDealAsync([FromRoute] string craftsmanId, [FromRoute]int dealId)
    {
        try
        {
            await _dealRepository.SetDealToAcceptedAsync(craftsmanId, dealId);
            return Ok("Deal accepted.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error accepting the deal offer id {dealId}");
            if (e is RowNotInTableException || e is ArgumentException)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(500, "Error accepting the deal.");
        }
    }

    [HttpPatch("close/{dealId}/{userId}")]
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
                return BadRequest($"Invalid parameters, {e.Message}");
            }

            return StatusCode(500, "Something went wrong.");
        }
    }
}