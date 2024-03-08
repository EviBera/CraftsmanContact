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
    
    
    public async Task<IdentityResult> RegisterUserAsync(RegisterModel model)
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
        return result;
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new ArgumentException("This user does not exist.");
        }
            
        await _userManager.DeleteAsync(user);
    }

    public async Task UpdateUserAsync(string userId, RegisterModel newUserData)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ArgumentException("This user does not exist.");
        }

        user.FirstName = newUserData.FirstName;
        user.LastName = newUserData.LastName;
        user.Email = newUserData.Email;
        user.PhoneNumber = newUserData.PhoneNumber;

        await _userManager.UpdateAsync(user);
    }

    public async Task<AppUser> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new ArgumentException("This user does not exist.");
        }

        return user;

    }
}