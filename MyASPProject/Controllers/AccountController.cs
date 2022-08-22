using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.Services;
using MyASPProject.ViewModels;

namespace MyASPProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly IAccount _account;

        public AccountController(UserManager<CustomIdentityUser> userManager,
            SignInManager<CustomIdentityUser> signInManager,
            IAccount account)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _account = account;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new CustomIdentityUser { 
                    UserName = model.Email, 
                    Email = model.Email,
                    FullName = model.FullName,
                    Address = model.Address,
                    City = model.City
                };
                var result = await _account.Register(user, model.Password);
                if (result)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            if(!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.returnUrl = returnUrl;
            }
            
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _account.Login(model);
                if (result)
                {
                    var user = new CustomIdentityUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };
                    

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                }
            }
               
            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} sudah digunakan, pilih email yg lain...");
            }
        }


    }
}
