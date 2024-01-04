using Microsoft.AspNetCore.Identity;

namespace UserAPI.Models
{
    public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public User() : base()
        {
        }
    }
}
