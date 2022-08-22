using MyASPProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MyASPProject.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
