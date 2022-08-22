using MyASPProject.Models;

namespace MyASPProject.ViewModels
{
    public class RestaurantViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public string Username { get; set; }
    }
}
