using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CraftsmanContact.Controllers;

[Route("api/[controller]")]
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

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(RegisterModel model)
    {
        try
        {
            var result = await _userRepository.RegisterUserAsync(model);
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

    [HttpDelete("Delete/{userId}")]
    public async Task<ActionResult> DeleteUserAsync([FromRoute] string userId)
    {
        try
        {
            await _userRepository.DeleteUserAsync(userId);
            return Ok("User deleted successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error deleting user with id {userId}");
            if (e is ArgumentException)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(500, "Something went wrong.");
        }
    }

    [HttpPut("Update/{userId}")]
    public async Task<ActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] RegisterModel updatedModel)
    {
        try
        {
            await _userRepository.UpdateUserAsync(userId, updatedModel);
            return Ok("User updated successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating user {userId} data.");
            if (e is ArgumentException)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(500, "Something went wrong.");
        }
    }

    [HttpGet("GetById/{userId}")]
    public async Task<ActionResult<AppUser?>> GetUserByIdAsync([FromRoute] string userId)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting user {userId}");
            if (e is ArgumentException)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(500, "Error getting user.");
        }
    }

}