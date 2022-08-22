using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIdentityProvider.Model;
using MyIdentityProvider.ViewModels;
using StudentCourse.DATA.DAL;
using StudentCourse.Domain;
using StudentCourseWebAPI.DTO;
using System.Text;

namespace MyIdentityProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public UserController(UserManager<CustomIdentityUser> userManager,
            SignInManager<CustomIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserViewModel userData)
        {
            try
            {
                var result = await _userManager.CreateAsync(userData.user, userData.password);
                if (!result.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    var errors = result.Errors;
                    foreach (var error in errors)
                    {
                        sb.Append($"{error.Code} - {error.Description} \n");
                    }
                    throw new Exception($"Error: {sb.ToString()}");
                }
                return Ok($"Registrasi user {userData.user.UserName} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false);
                if(result.Succeeded == true)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Invalid Username Or Password");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
