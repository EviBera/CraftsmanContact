using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CraftsmanContact.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<UserController> _logger;

    public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<UserController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(RegisterModel model)
    {
        try
        {
            var user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                PasswordHash = model.Password
            };
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Created();

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
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("This user does not exist.");
            }
            
            await _userManager.DeleteAsync(user);
            return Ok("User deleted successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error deleting user with id {userId}");
            return StatusCode(500, "Something went wrong.");
        }
    }

    [HttpPut("Update/{userId}")]
    public async Task<ActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] RegisterModel updatedModel)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("This user does not exist.");
            }

            user.FirstName = updatedModel.FirstName;
            user.LastName = updatedModel.LastName;
            user.Email = updatedModel.Email;
            user.PhoneNumber = updatedModel.PhoneNumber;

            await _userManager.UpdateAsync(user);

            return Ok("User updated successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating user {userId} data.");
            return StatusCode(500, "Something went wrong.");
        }
    }

    [HttpGet("GetById/{userId}")]
    public async Task<ActionResult<AppUser?>> GetUserByIdAsync([FromRoute] string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("This user does not exist.");
            }

            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting user {userId}");
            return StatusCode(500, "Error getting user.");
        }
    }

}