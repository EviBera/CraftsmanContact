using CraftsmanContact.DTOs.Deal;

namespace CraftsmanContact.Services.Repository;

public interface IDealRepository
{
    Task CreateDealAsync(CreateDealRequestDto dealDto);
    Task<IEnumerable<DealDto>> GetDealsByUserAsync(string userId);
    Task<DealDto> GetDealByIdAsync(int dealId);
    Task SetDealToAcceptedAsync(string craftsmanId, int dealId);
    Task SetDealClosedAsync(int dealId, string userId);
    Task<IEnumerable<DealDto>> GetDeadDealsAsync();
}