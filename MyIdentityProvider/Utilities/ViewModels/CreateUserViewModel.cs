using MyIdentityProvider.Model;

namespace MyIdentityProvider.ViewModels
{
    public class CreateUserViewModel
    {
        public CustomIdentityUser user { get; set; }
        public string password { get; set; }
    }
}
