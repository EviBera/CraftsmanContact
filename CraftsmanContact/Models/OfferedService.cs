using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.Models;

public class OfferedService
{
    [Key] 
    public int OfferedServiceId { get; init; }

    [StringLength(50)] 
    public string OfferedServiceName { get; set; } = String.Empty;
    [StringLength(300)]
    public string? OfferedServiceDescription { get; set; }

    public virtual ICollection<UserOfferedService> UserOfferedServices { get; set; } = new HashSet<UserOfferedService>();
    
}