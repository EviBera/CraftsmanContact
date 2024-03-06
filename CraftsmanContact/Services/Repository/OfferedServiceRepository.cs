using CraftsmanContact.Data;
using CraftsmanContact.Models;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Services.Repository;

public class OfferedServiceRepository : IOfferedServiceRepository
{
    private readonly CraftsmanContactContext _dbContext;

    public OfferedServiceRepository(CraftsmanContactContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<OfferedService>> GetAllAsync()
    {
        var services = await _dbContext.OfferedServices.ToListAsync();
        return services;
    }

    public async Task RegisterAsync(OfferedService offeredService)
    {
        await _dbContext.OfferedServices.AddAsync(offeredService);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<OfferedService?> GetByIdAsync(int id)
    {
        var service = await _dbContext.OfferedServices.FindAsync(id);
        return service;
    }

    public async Task UpdateAsync(int id, string? newName, string? newDescription)
    {
        var serviceToUpdate = await _dbContext.OfferedServices.FindAsync(id);
        
        if (serviceToUpdate != null)
        {
            if (newName != null)
            {
                serviceToUpdate.Name = newName;
            }
    
            if (newDescription != null)
            {
                serviceToUpdate.Description = newDescription;
            }

            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Invalid Id");
        }
        
    }

    public async Task DeleteAsync(int id)
    {
        var serviceToDelete = await _dbContext.OfferedServices.FindAsync(id);
        
        if (serviceToDelete == null)
        {
            throw new ArgumentException("Invalid Id");
        }
        else
        {
            await _dbContext.OfferedServices.Where(item => item.Id == id).ExecuteDeleteAsync();
        }
    }
    
}