using Microsoft.AspNetCore.Identity;
using MyASPProject.Models;

namespace MyASPProject.Services
{
    public class PenggunaServices : IPengguna
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        public PenggunaServices(UserManager<CustomIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddUserToRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            try
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public Task<IEnumerable<string>> GetAllUser()
        {
            throw new NotImplementedException();
        }
    }
}
