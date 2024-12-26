using Microsoft.AspNetCore.Identity;

namespace RealEstate.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public UserProfiles UserProfile { get; set; }
    }
}
