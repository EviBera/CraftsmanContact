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


    //Must handle nonexistent user!!!
    [HttpGet("GetByUser/{userId}")]
    public async Task<ActionResult<List<Deal>>> GetDealsByUser([FromRoute]string userId)
    {
        try
        {
            var deals = await _dealRepository.GetDealsByUserAsync(userId);
            return Ok(deals);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting deals for user {userId}.");
            return NotFound($"Error getting deals, {e.Message}");
        }
    }

    [HttpGet("GetById/{dealId}")]
    public async Task<ActionResult<Deal>> GetDealById([FromRoute]int dealId)
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
    public async Task<ActionResult> AddNewDeal([FromBody] Deal deal)
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
}