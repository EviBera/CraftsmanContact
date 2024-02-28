using CraftsmanContact.Data;
using CraftsmanContact.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Services;

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
}