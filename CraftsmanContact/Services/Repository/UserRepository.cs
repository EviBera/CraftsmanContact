using System.Data;
using CraftsmanContact.DTOs.User;
using CraftsmanContact.Mappers;
using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity;

namespace CraftsmanContact.Services.Repository;

public class UserRepository : IUserRepository
{
    
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    
    public async Task<IdentityResult> RegisterUserAsync(RegisterUserRequestDto requestDto)
    {
        var user = requestDto.ToAppUserFromRegisterUserRequestDto();
        var result = await _userManager.CreateAsync(user, user.PasswordHash);
        return result;
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new RowNotInTableException();
        }
            
        await _userManager.DeleteAsync(user);
    }

    public async Task UpdateUserAsync(string userId, UpdateUserRequestDto requestDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new RowNotInTableException();
        }

        user.FirstName = requestDto.FirstName;
        user.LastName = requestDto.LastName;
        user.Email = requestDto.Email;
        user.PhoneNumber = requestDto.PhoneNumber;

        await _userManager.UpdateAsync(user);
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new RowNotInTableException();
        }

        return user.ToUserDto();
    }
}