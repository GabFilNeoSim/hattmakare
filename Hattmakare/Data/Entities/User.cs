using Microsoft.AspNetCore.Identity;

namespace Hattmakare.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation props
        public virtual ICollection<OrderHat> OrderHats { get; set; }
    }
}
