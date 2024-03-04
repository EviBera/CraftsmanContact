using Microsoft.AspNetCore.Identity;

namespace CraftsmanContact.Models;

public class AppUser : IdentityUser
{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        //Navigation property for the offered services
        public virtual ICollection<UserOfferedService> UserOfferedServices { get; set; }

        public AppUser()
        {
                UserOfferedServices = new HashSet<UserOfferedService>();
        }
}
