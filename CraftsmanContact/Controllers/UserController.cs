using System.Data;
using CraftsmanContact.DTOs.OfferedService;
using CraftsmanContact.DTOs.User;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CraftsmanContact.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserRepository userRepository, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterUserRequestDto requestDto)
    {
        try
        {
            var result = await _userRepository.RegisterUserAsync(requestDto);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok("User registered successfully.");

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Registering new user has failed.");
            return StatusCode(500, $"Something went wrong., {e.Message}");
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] string userId)
    {
        try
        {
            await _userRepository.DeleteUserAsync(userId);
            return Ok("User deleted successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error deleting user with id {userId}");
            if (e is RowNotInTableException)
            {
                return BadRequest("This user does not exist.");
            }
            return StatusCode(500, "Something went wrong.");
        }
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] UpdateUserRequestDto requestDto)
    {
        try
        {
            await _userRepository.UpdateUserAsync(userId, requestDto);
            return Ok("User updated successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating user {userId} data.");
            if (e is RowNotInTableException)
            {
                return BadRequest("This user does not exist");
            }
            return StatusCode(500, "Something went wrong.");
        }
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto?>> GetUserByIdAsync([FromRoute] string userId)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting user {userId}");
            if (e is RowNotInTableException)
            {
                return BadRequest("This user does not exist.");
            }
            return StatusCode(500, "Error getting user.");
        }
    }

    [HttpPatch("registerservice/{userId}/{serviceId}")]
    public async Task<IActionResult> RegisterServiceAsOfferedByUserAsync([FromRoute] string userId,
        [FromRoute] int serviceId)
    {
        try
        {
            await _userRepository.RegisterServiceForCraftsmanAsync(userId, serviceId);
            return Ok("Service registered successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error registering service id {serviceId} for user id {userId}");
            if (e is RowNotInTableException)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(500, "Error registering service for user.");
        }
    }

    [HttpPatch("removeservice/{userId}/{serviceId}")]
    public async Task<IActionResult> RemoveOfferedServiceFromCraftsmanAsync([FromRoute] string userId, [FromRoute] int serviceId)
    {
        try
        {
            await _userRepository.RemoveServiceOfCraftsmanAsync(userId, serviceId);
            return Ok("Service deleted successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error removing service id {serviceId} for user id {userId}");
            if (e is RowNotInTableException)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(500, $"Error removing service for user, {e.Message}");
        }
    }

    [HttpGet("craftsmenbyservice/{serviceId}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetCraftsmenByServiceAsync([FromRoute] int serviceId)
    {
        try
        {
            IEnumerable<UserDto> craftsmen = await _userRepository.GetCraftsmenByServiceIdAsync(serviceId);
            return Ok(craftsmen);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting craftsmen by service id {serviceId}.");
            if (e is RowNotInTableException)
            {
                return BadRequest("This service does not exist.");
            }
            return StatusCode(500, "Error getting craftsmen.");
        }
    }

    [HttpGet("services/{userId}")]
    public async Task<ActionResult<OfferedServiceDto>> GetServicesOfUserAsync([FromRoute] string userId)
    {
        try
        {
            var services = await _userRepository.GetServicesOfUserAsync(userId);
            return Ok(services);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting services of user id {userId}.");
            if (e is RowNotInTableException)
            {
                return BadRequest("This user does not exist.");
            }
            return StatusCode(500, "Error getting services of craftsman.");
        }
    }
    
}   
