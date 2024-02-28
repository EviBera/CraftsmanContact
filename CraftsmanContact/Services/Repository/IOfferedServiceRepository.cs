using CraftsmanContact.Models;
using Microsoft.AspNetCore.Mvc;

namespace CraftsmanContact.Services;

public interface IOfferedServiceRepository
{
    Task<IEnumerable<OfferedService>> GetAllAsync();
    Task RegisterAsync(OfferedService offeredService);
}