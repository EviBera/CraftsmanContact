using CraftsmanContact.DTOs.User;
using CraftsmanContact.Models;
using CraftsmanContact.Services.AuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CraftsmanContact.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AuthController> _logger;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager, ILogger<AuthController> logger, ITokenService tokenService)
    {
        _userManager = userManager;
        _logger = logger;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequestDto requestDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(418, ModelState);

            var appUser = new AppUser
            {
                UserName = requestDto.Email,
                Email = requestDto.Email,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                PhoneNumber = requestDto.PhoneNumber
            };

            var createdUser = await _userManager.CreateAsync(appUser, requestDto.Password);
            
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(
                        new NewUserDto
                        {
                            UserName = appUser.Email,
                            Email = appUser.Email,
                            LastName = appUser.LastName,
                            FirstName = appUser.FirstName,
                            Token = _tokenService.CreateToken(appUser)
                        });
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Registering new user has failed.");
            return StatusCode(500, $"Something went wrong., {e.Message}");
        }
    }
}