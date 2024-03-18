using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CraftsmanContact.Models;

public class AppUser : IdentityUser
{
        [StringLength(50), Required] 
        public string FirstName { get; set; } = String.Empty;
        [StringLength(50), Required]
        public string LastName { get; set; } = String.Empty;
        
        //Navigation property for the offered services
        public virtual ICollection<OfferedService> OfferedServices { get; set; } = new HashSet<OfferedService>();
        
}
