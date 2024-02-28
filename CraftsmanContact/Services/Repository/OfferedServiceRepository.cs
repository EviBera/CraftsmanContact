using CraftsmanContact.Data;
using CraftsmanContact.Models;

namespace CraftsmanContact.Services;

public class OfferedServiceRepository : IOfferedServiceRepository
{
    private readonly OfferedServiceContext _dbContext;

    public OfferedServiceRepository(OfferedServiceContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<OfferedService> GetAll()
    {
        return _dbContext.OfferedServices.ToList();
    }
}