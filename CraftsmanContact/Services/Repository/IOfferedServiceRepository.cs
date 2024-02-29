using CraftsmanContact.Models;

namespace CraftsmanContact.Services.Repository;

public interface IOfferedServiceRepository
{
    Task<IEnumerable<OfferedService>> GetAllAsync();
    Task RegisterAsync(OfferedService offeredService);
    Task<OfferedService?> GetByIdAsync(int id);
}