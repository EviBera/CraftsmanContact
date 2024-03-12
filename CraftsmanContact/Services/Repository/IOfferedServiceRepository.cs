using CraftsmanContact.DTOs;
using CraftsmanContact.Models;

namespace CraftsmanContact.Services.Repository;

public interface IOfferedServiceRepository
{
    Task<IEnumerable<OfferedServiceDto>> GetAllAsync();
    Task<OfferedService> RegisterAsync(CreateRequestOfferedServiceDto offeredServiceDto);
    Task<OfferedServiceDto?> GetByIdAsync(int id);
    Task<OfferedServiceDto> UpdateAsync(int id, UpdateRequestOfferedServiceDto offeredServiceDto);
    Task DeleteAsync(int id);
}