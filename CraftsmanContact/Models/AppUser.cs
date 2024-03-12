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
        public ICollection<UserOfferedService> UserOfferedServices { get; set; } = new HashSet<UserOfferedService>();
        
}
