using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.Models;

public class OfferedService
{
    [Key] 
    public int OfferedServiceId { get; set; }
    [StringLength(50)]
    public string OfferedServiceName { get; set; }
    [StringLength(300)]
    public string? OfferedServiceDescription { get; set; }

    public ICollection<UserOfferedService> UserOfferedServices { get; set; } = new HashSet<UserOfferedService>();
    
}