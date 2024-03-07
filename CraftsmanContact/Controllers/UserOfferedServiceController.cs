using CraftsmanContact.Data;
using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOfferedServiceController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<UserOfferedServiceController> _logger;
    private readonly UsersContext _usersContext;
    private readonly IOfferedServiceRepository _offeredServiceRepository;

    public UserOfferedServiceController(UserManager<AppUser> userManager, ILogger<UserOfferedServiceController> logger, 
        UsersContext context, IOfferedServiceRepository offeredServiceRepository)
    {
        _userManager = userManager;
        _logger = logger;
        _usersContext = context;
        _offeredServiceRepository = offeredServiceRepository;
    }

    //If found => loops endlessly
    [HttpGet("GetCraftsmen/{serviceId}")]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetCraftsmenByOfferedServiceAsync([FromRoute] int serviceId)
    {
        try
        {
            HashSet<AppUser> users = new HashSet<AppUser>();
            var services = await _usersContext.UserOfferedServices.Where(s => s.OfferedServiceId == serviceId)
                .ToListAsync();
            
            foreach (var service in services)
            {
                var user = await _userManager.FindByIdAsync(service.AppUserId);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            if (users.Count == 0)
            {
                return NoContent();
            }
            
            return Ok(users);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting craftsmen by service {serviceId}");
            return StatusCode(500, "Error getting craftsmen.");
        }
    }

    [HttpPost("Register/{userId}/{serviceId}")]
    public async Task<ActionResult> RegisterOfferedServiceAsync([FromRoute] string userId, [FromRoute] int serviceId)
    {
        try
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Invalid user Id.");
            }

            var service = await _offeredServiceRepository.GetByIdAsync(serviceId);
            if (service == null)
            {
                return BadRequest("Invalid service Id.");
            }
            
            UserOfferedService newService = new UserOfferedService()
            {
                AppUserId = userId,
                OfferedServiceId = serviceId
            };
            await _usersContext.UserOfferedServices.AddAsync(newService);
            await _usersContext.SaveChangesAsync();
            
            return Created();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error registering the user offered service");
            return StatusCode(500, "Error registering the service");
        }
    }

}