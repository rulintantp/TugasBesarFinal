using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyASPProject.Models;

namespace MyASPProject.Data
{
    public class RestaurantDbContext : IdentityDbContext<CustomIdentityUser>
    {
        public RestaurantDbContext(DbContextOptions options):base(options)  
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
