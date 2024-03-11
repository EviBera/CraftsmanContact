using System.ComponentModel.DataAnnotations.Schema;

namespace CraftsmanContact.Models;

[Table("UsersAndServicesJoinedTable")]
public class UserOfferedService
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public int OfferedServiceId { get; set; }
    public OfferedService OfferedService { get; set; }
    
}