using MyASPProject.Models;

namespace MyASPProject.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurants;
        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>()
            {
                new Restaurant{Id=1,Name="Steak ABC"},
                new Restaurant{Id=2,Name="Pasta Banget"},
                new Restaurant{Id=3,Name="Soto Ayam Kadipiro"}
            };
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(restaurant);
            return restaurant;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(r => r.Name);
        }

        public Restaurant GetById(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }
    }
}
