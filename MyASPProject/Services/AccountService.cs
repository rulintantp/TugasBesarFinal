using Microsoft.AspNetCore.Identity;
using MyASPProject.Models;
using MyASPProject.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MyASPProject.Services
{
    public class AccountService : IAccount
    {
        public async Task<bool> Register(CustomIdentityUser user, string password)
        {
            bool isSuccess = false;
            CreateUserViewModel request = new CreateUserViewModel();
            request.user = user;
            request.password = password;

            StringContent content =
                   new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"); //menampung data berupa c# yang diconvert menjadi json

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/User", content))  //call Backend URL
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        isSuccess = true;
                    }
                }
            }
            return isSuccess; //return to controller
        }

        public async Task<bool> Login(LoginViewModel model)
        {
            bool isSuccess = false;

            StringContent content =
                   new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/User/Login", content))  //call Backend URL
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        isSuccess = true;
                    }
                }
            }
            return isSuccess; //return to controller
        }
    }
}
