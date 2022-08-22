using Microsoft.AspNetCore.Identity;

namespace MyASPProject.ViewModels
{
    public class RegistrationResponseViewModel
    {
        public bool Succeeded { get; protected set; }
        public IEnumerable<IdentityError> Errors { get; }
    }
}
