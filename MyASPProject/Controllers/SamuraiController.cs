using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.Services;

namespace MyASPProject.Controllers
{
    public class SamuraiController : Controller
    {
        private readonly ISamurai _samurai;
        public SamuraiController(ISamurai samurai)
        {
            _samurai = samurai;
        }

        public async Task<IActionResult> Index(string? name)
        {

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            IEnumerable<Samurai> model;
            if (name == null)
            {
                model = await _samurai.GetAll();
            }
            else
            {
                model = await _samurai.GetByName(name);
            }
            
            return View(model);
        }

        public async Task<IActionResult> WithQuote()
        {
            var model = await _samurai.GetSamuraiWithQuotes();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _samurai.GetById(id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Samurai samurai)
        {
            try
            {
                var result = await _samurai.Insert(samurai);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data samurai {result.Name}</div>";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }
            
        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await _samurai.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Samurai samurai)
        {
            try
            {
                var result = await _samurai.Update(samurai);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengupdate data samurai {result.Name}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _samurai.GetById(id);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _samurai.Delete(id);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mendelete data samurai id: {id}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        public async Task<IActionResult> GetWeather()
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }

                var model = await _samurai.GetAllWeather(myToken);

            return View(model);
        }
    } 
}
