using Microsoft.AspNetCore.Mvc;
using MyASPProject.Utilities;
using System.ComponentModel.DataAnnotations;

namespace MyASPProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain:"rapidtech.id",ErrorMessage ="Domain harus rapidtech.id")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirmation Password")]
        [Compare("Password",ErrorMessage ="Password dan Confirm Password tidak sama")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

    }
}
