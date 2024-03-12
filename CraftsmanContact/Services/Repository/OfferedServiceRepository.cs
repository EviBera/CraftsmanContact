using System.Data;
using CraftsmanContact.Data;
using CraftsmanContact.DTOs;
using CraftsmanContact.Mappers;
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
    
    public async Task<IEnumerable<OfferedServiceDto>> GetAllAsync()
    {
        var services = await _dbContext.OfferedServices.Select(s => s.ToOfferedServiceDto()).ToListAsync();
        
        return services;
    }

    public async Task<OfferedService> RegisterAsync(CreateRequestOfferedServiceDto serviceDto)
    {
        var service = serviceDto.ToOfferedServiceFromCreateRequestOfferedServiceDto();
        await _dbContext.OfferedServices.AddAsync(service);
        await _dbContext.SaveChangesAsync();

        return service;
    }

    public async Task<OfferedServiceDto?> GetByIdAsync(int id)
    {
        OfferedService? service = await _dbContext.OfferedServices.FirstOrDefaultAsync(s => s.OfferedServiceId == id);
        
        if (service != null)
        {
            return service.ToOfferedServiceDto();
        }

        throw new RowNotInTableException();
    }

    public async Task<OfferedServiceDto> UpdateAsync(int id, UpdateRequestOfferedServiceDto serviceDto)
    {
        var serviceToUpdate = await _dbContext.OfferedServices.FirstOrDefaultAsync(s => s.OfferedServiceId == id);

        if (serviceToUpdate == null)
        {
            throw new RowNotInTableException();
        }
        
        serviceToUpdate.OfferedServiceName = serviceDto.OfferedServiceName;
        serviceToUpdate.OfferedServiceDescription = serviceDto.OfferedServiceDescription;
        await _dbContext.SaveChangesAsync();

        return serviceToUpdate.ToOfferedServiceDto();
    }

    public async Task DeleteAsync(int id)
    {
        var serviceToDelete = await _dbContext.OfferedServices.FindAsync(id);
        
        if (serviceToDelete == null)
        {
            throw new RowNotInTableException();
        }

        await _dbContext.OfferedServices.Where(item => item.OfferedServiceId == id).ExecuteDeleteAsync();

    }
    
}