using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.Services;
using MyASPProject.ViewModels;

namespace MyASPProject.Controllers
{
   
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantData _resto;
        public RestaurantsController(IRestaurantData resto)
        {
            _resto = resto;
        }

        [Authorize(Roles = "regular user,admin")]
        public IActionResult Index()
        {
            /*var model = new Restaurant
            {
                Id = 1,
                Name = "Steak ABC"
            };

            ViewData["username"] = "Erick Kurniawan";
            ViewBag.Role = "Administrator";

            return View(model);*/

            /*var models = _resto.GetAll();
            return View(models);*/

            var models = new RestaurantViewModel();
            models.Restaurants = _resto.GetAll();
            models.Username = "erickkurniawan";

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];

            return View(models);
        }

        [Authorize(Roles = "regular user,admin")]
        public IActionResult Details(int id)
        {
            var model = _resto.GetById(id);
            if(model == null)
            {
                TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>Data Restaurant dengan id {id} tidak deitemukan</div>";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var newResto = new Restaurant()
                {
                    Name = model.Name,
                    Cuisine = model.Cuisine
                };
                _resto.Add(newResto);

                TempData["pesan"] =
                    $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data restaurant {model.Name}</div>";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
