using System.Data;
using CraftsmanContact.Data;
using CraftsmanContact.DTOs.OfferedService;
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
        var user = await _dbContext.Users
            .Include(u => u.OfferedServices)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new RowNotInTableException();
        }

        return user.ToUserDto();
    }

    public async Task RegisterServiceForCraftsmanAsync(string userId, int serviceId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        _dbContext.Entry(user).Collection(u => u.OfferedServices).Load();
        
        if (user == null)
        {
            throw new RowNotInTableException("This user does not exist.");
        }

        var service = await _dbContext.OfferedServices.FindAsync(serviceId);
        _dbContext.Entry(service).Collection(s => s.AppUsers).Load();
        
        if (service == null)
        {
            throw new RowNotInTableException("This service does not exist.");
        }
        
        user.OfferedServices.Add(service);
        service.AppUsers.Add(user); 
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveServiceOfCraftsmanAsync(string userId, int serviceId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        _dbContext.Entry(user).Collection(u => u.OfferedServices).Load();
        
        if (user == null)
        {
            throw new RowNotInTableException("This user does not exist.");
        }

        var service = await _dbContext.OfferedServices.FindAsync(serviceId);

        if (service == null)
        {
            throw new RowNotInTableException("This service does not exist.");
        }

        var needlessService = user.OfferedServices.FirstOrDefault(s => s.OfferedServiceId == serviceId);

        if (needlessService == null)
        {
            throw new RowNotInTableException("The user does not offer this service.");
        }

        user.OfferedServices.Remove(needlessService);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserDto>> GetCraftsmenByServiceIdAsync(int serviceId)
    {
        var service = await _dbContext.OfferedServices.FindAsync(serviceId);
        
        if (service == null)
        {
            throw new RowNotInTableException();
        }
        
        _dbContext.Entry(service).Collection(s => s.AppUsers).Load();
        var craftsmen = service.AppUsers.Select(u => u.ToUserDto());
        
        return craftsmen;
    }

    public async Task<IEnumerable<OfferedServiceDto>> GetServicesOfUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        _dbContext.Entry(user).Collection(u => u.OfferedServices).Load();

        if (user == null)
        {
            throw new RowNotInTableException("This user does not exist.");
        }

        var services = user.OfferedServices.Select(s => s.ToOfferedServiceDto());
        return services;
    }
}