using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.Models;

public class OfferedService
{
    [Key] 
    public int OfferedServiceId { get; init; }
    public string OfferedServiceName { get; set; } = String.Empty;
    public string? OfferedServiceDescription { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; set; } = new HashSet<AppUser>();
    
}