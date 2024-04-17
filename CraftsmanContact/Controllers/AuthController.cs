using CraftsmanContact.DTOs.User;
using CraftsmanContact.Models;
using CraftsmanContact.Services.AuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<AuthController> _logger;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AuthController> logger, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
                            Token = _tokenService.CreateToken(appUser, "User")
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

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid email or password");
        }
        
        var roles = await _userManager.GetRolesAsync(user);
        
        return Ok(
            new NewUserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Email,
                Token = _tokenService.CreateToken(user, roles[0])
            });
    }
}