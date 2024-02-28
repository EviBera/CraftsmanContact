using CraftsmanContact.Data;
using CraftsmanContact.Models;
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
}