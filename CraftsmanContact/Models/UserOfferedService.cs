namespace CraftsmanContact.Models;

public class UserOfferedService
{
    public int Id { get; set; }

    // Foreign key for AppUser
    public string AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }

    // Foreign key for OfferedService
    public int OfferedServiceId { get; set; }
    
}