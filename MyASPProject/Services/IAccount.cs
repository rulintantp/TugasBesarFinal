using Microsoft.AspNetCore.Identity;
using MyASPProject.Models;
using MyASPProject.ViewModels;

namespace MyASPProject.Services
{
    public interface IAccount
    {
        Task<bool> Register(CustomIdentityUser user, string Password);
        Task<bool> Login(LoginViewModel model);
    }
}
