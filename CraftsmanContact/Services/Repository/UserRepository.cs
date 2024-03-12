using System.Data;
using CraftsmanContact.Data;
using CraftsmanContact.DTOs.User;
using CraftsmanContact.Mappers;
using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Services.Repository;

public class UserRepository : IUserRepository
{
    
    private readonly UserManager<AppUser> _userManager;
    private readonly CraftsmanContactContext _dbContext;

    public UserRepository(UserManager<AppUser> userManager, CraftsmanContactContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
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

    public async Task RegisterServiceForCraftsmanAsync(string userId, int serviceId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new RowNotInTableException("This user does not exist.");
        }

        var service = await _dbContext.OfferedServices.FindAsync(serviceId);

        if (service == null)
        {
            throw new RowNotInTableException("This service does not exist.");
        }

        var newRecord = new UserOfferedService
        {
            AppUserId = userId,
            OfferedServiceId = serviceId
        };
        await _dbContext.UsersAndServicesJoinedTable.AddAsync(newRecord);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserDto>> GetCraftsmenByIdAsync(int serviceId)
    {
        var service = await _dbContext.OfferedServices.FindAsync(serviceId);
        
        if (service == null)
        {
            throw new RowNotInTableException();
        }

        var craftsmen = await _dbContext.UsersAndServicesJoinedTable.
            Where(item => item.OfferedServiceId == serviceId).Select(x => x.AppUser.ToUserDto())
            .ToListAsync();
        
        return craftsmen;
    }
}