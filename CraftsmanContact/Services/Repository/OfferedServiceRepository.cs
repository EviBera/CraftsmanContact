using CraftsmanContact.Data;
using CraftsmanContact.Models;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Services.Repository;

public class OfferedServiceRepository : IOfferedServiceRepository
{
    private readonly OfferedServiceContext _dbContext;

    public OfferedServiceRepository(OfferedServiceContext dbContext)
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
        await _dbContext.AddAsync(offeredService);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<OfferedService?> GetByIdAsync(int id)
    {
        var service = await _dbContext.OfferedServices.FindAsync(id);
        return service;
    }
}