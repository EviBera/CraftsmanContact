using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity;

namespace CraftsmanContact.Services.Repository;

public interface IUserRepository
{
    Task<IdentityResult> RegisterUserAsync(RegisterModel model);
    Task DeleteUserAsync(string userId);
    Task UpdateUserAsync(string userId, RegisterModel newUserData);
    Task<AppUser> GetUserByIdAsync(string userId);
}