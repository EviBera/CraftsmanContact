using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CraftsmanContact.Models;

public class AppUser : IdentityUser
{
        [StringLength(50), Required]
        public string FirstName { get; set; }
        [StringLength(50), Required]
        public string LastName { get; set; }
        
        //Navigation property for the offered services
        public virtual ICollection<UserOfferedService> UserOfferedServices { get; set; }

        public AppUser()
        {
                UserOfferedServices = new HashSet<UserOfferedService>();
        }
}
