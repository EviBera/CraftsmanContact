using CraftsmanContact.DTOs.OfferedService;
using CraftsmanContact.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace CraftsmanContact.Services.Repository;

public interface IUserRepository
{
    Task<IdentityResult> RegisterUserAsync(RegisterUserRequestDto requestDto);
    Task DeleteUserAsync(string userId);
    Task UpdateUserAsync(string userId, UpdateUserRequestDto requestDto);
    Task<UserDto> GetUserByIdAsync(string userId);
    Task RegisterServiceForCraftsmanAsync(string userId, int serviceId);
    Task RemoveServiceOfCraftsmanAsync(string userId, int serviceId);
    Task<IEnumerable<UserDto>> GetCraftsmenByServiceIdAsync(int serviceId);
    Task<IEnumerable<OfferedServiceDto>> GetServicesOfUserAsync(string userId);
}