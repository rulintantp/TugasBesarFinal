using Microsoft.AspNetCore.Identity;

namespace MyASPProject.Models
{
    public class CustomIdentityUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
