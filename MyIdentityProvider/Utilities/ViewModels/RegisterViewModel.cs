using Microsoft.AspNetCore.Mvc;
using MyMyIdentityProvider.Utilities;
using System.ComponentModel.DataAnnotations;

namespace MyIdentityProvider.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

    }
}
