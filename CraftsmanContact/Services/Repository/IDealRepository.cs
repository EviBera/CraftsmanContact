using CraftsmanContact.Models;

namespace CraftsmanContact.Services.Repository;

public interface IDealRepository
{
    Task CreateDealAsync(Deal deal);
    Task<IEnumerable<Deal>> GetDealsByUserAsync(string userId);
    Task<Deal> GetDealByIdAsync(int dealId);
    Task SetDealToAcceptedAsync(int dealId);
    Task SetDealClosedAsync(int dealId, string userId);
}