using MyASPProject.Models;

namespace MyASPProject.ViewModels
{
    public class CreateUserViewModel
    {
        public CustomIdentityUser user { get; set; }
        public string password { get; set;}
    }
}
