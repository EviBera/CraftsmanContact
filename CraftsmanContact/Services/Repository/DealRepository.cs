using System.Data;
using CraftsmanContact.Data;
using CraftsmanContact.DTOs.Deal;
using CraftsmanContact.Mappers;
using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Services.Repository;

public class DealRepository : IDealRepository
{
    private readonly CraftsmanContactContext _dbContext;
    private readonly UserManager<AppUser> _userManager;

    public DealRepository(CraftsmanContactContext dbContext, UserManager<AppUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    public async Task CreateDealAsync(CreateDealRequestDto dealDto)
    {
        var craftsman = await _userManager.FindByIdAsync(dealDto.CraftsmanId);
        if (craftsman == null)
        {
            throw new RowNotInTableException("The craftsman does not exist.");
        }

        var client = await _userManager.FindByIdAsync(dealDto.ClientId);
        if (client == null)
        {
            throw new RowNotInTableException("The client does not exist.");
        }
        
        _dbContext.Entry(craftsman).Collection(u => u.OfferedServices).Load();
        var service = craftsman.OfferedServices.FirstOrDefault(s => s.OfferedServiceId == dealDto.OfferedServiceId);
        if (service == null)
        {
            throw new RowNotInTableException("The craftsman does not offer this service.");
        }
            
        await _dbContext.Deals.AddAsync(dealDto.ToDealFromCreateDealRequestDto());
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<DealDto>> GetDealsByUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new RowNotInTableException();
        }
        
        var dealsAsClient = await _dbContext.Deals.Where(d => d.ClientId == userId).Select(d => d.ToDealDto()).ToListAsync();
        var dealsAsCraftsman = await _dbContext.Deals.Where(d => d.CraftsmanId == userId).Select(d => d.ToDealDto()).ToListAsync();
        
        return dealsAsClient.Concat(dealsAsCraftsman);
    }

    public async Task<DealDto> GetDealByIdAsync(int dealId)
    {
        var deal = await _dbContext.Deals.FindAsync(dealId);
        if (deal == null)
        {
            throw new RowNotInTableException();
        }
        return deal.ToDealDto();
    }

    public async Task SetDealToAcceptedAsync(string craftsmanId, int dealId)
    {
        var deal = await _dbContext.Deals.FindAsync(dealId);
        if (deal == null)
        {
            throw new RowNotInTableException("Deal does not exist.");
        }

        var craftsman = await _userManager.FindByIdAsync(deal.CraftsmanId);
        if (craftsman == null)
        {
            throw new RowNotInTableException("The assignee craftsman does not exist.");
        }

        if (deal.CraftsmanId != craftsmanId)
        {
            throw new ArgumentException("This user is not authorized to accept the offer.");
        }

        if (deal.IsAcceptedByCraftsman)
        {
            throw new ArgumentException("The offer is already accepted.");
        }

        deal.IsAcceptedByCraftsman = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task SetDealClosedAsync(int dealId, string userId)
    {
        var deal = await _dbContext.Deals.FindAsync(dealId);

        if (deal == null)
        {
            throw new RowNotInTableException("Deal does not exist.");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new RowNotInTableException("This user does not exist.");
        }

        if (deal.ClientId == userId && !deal.IsClosedByClient)
        {
            deal.IsClosedByClient = true;
        } 
        else if (deal.CraftsmanId == userId && !deal.IsClosedByCraftsman)
        {
            deal.IsClosedByCraftsman = true;
        }
        else
        {
            throw new ArgumentException("This user can not close the deal.");
        }

        await _dbContext.SaveChangesAsync();
    }
}