using CraftsmanContact.Models;

namespace CraftsmanContact.Services;

public interface IOfferedServiceRepository
{
    Task<IEnumerable<OfferedService>> GetAllAsync();
}