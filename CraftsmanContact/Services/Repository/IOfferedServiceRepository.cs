using CraftsmanContact.Models;

namespace CraftsmanContact.Services;

public interface IOfferedServiceRepository
{
    IEnumerable<OfferedService> GetAll();
}