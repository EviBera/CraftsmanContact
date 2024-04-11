using CraftsmanContact.DTOs.OfferedService;

namespace CraftsmanContact.DTOs.User;

public class UserDto
{
    public string Id { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Phone { get; set; } = String.Empty;
    
    public List<OfferedServiceDto>? OfferedServices { get; set; }
}