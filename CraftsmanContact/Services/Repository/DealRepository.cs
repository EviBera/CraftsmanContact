using System.Collections.Immutable;
using System.Data;
using CraftsmanContact.Data;
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
    
    public async Task CreateDealAsync(Deal deal)
    {
        await _dbContext.Deals.AddAsync(deal);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Deal>> GetDealsByUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new RowNotInTableException("This user does not exist.");
        }
        
        var dealsAsClient = await _dbContext.Deals.Where(d => d.ClientId == userId).ToListAsync();
        var dealsAsCraftsman = await _dbContext.Deals.Where(d => d.CraftsmanId == userId).ToListAsync();
        
        return dealsAsClient.Concat(dealsAsCraftsman);
    }

    public async Task<Deal> GetDealById(int dealId)
    {
        var deal = await _dbContext.Deals.FindAsync(dealId);
        return deal;
    }

    public async Task SetDealToAcceptedAsync(int dealId)
    {
        var deal = await _dbContext.Deals.FindAsync(dealId);

        if (deal == null)
        {
            throw new ArgumentException("Deal does not exist.");
        }

        deal.IsAcceptedByCraftsman = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task SetDealClosedAsync(int dealId, string userId)
    {
        var deal = await _dbContext.Deals.FindAsync(dealId);

        if (deal == null)
        {
            throw new ArgumentException("Deal does not exist.");
        }

        if (deal.ClientId == userId)
        {
            deal.IsClosedByClient = true;
        } 
        else if (deal.CraftsmanId == userId)
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