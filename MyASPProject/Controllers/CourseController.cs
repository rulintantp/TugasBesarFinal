using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.Services;

namespace MyASPProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourse _course;

        public CourseController(ICourse course)
        {
            _course = course;
        }
        public async Task<IActionResult> Index(int pageNumber, string? name)
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];

            string myToken = string.Empty;

            IEnumerable<Course> model;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            if (name == null)
            {
                model = await _course.GetAll(pageNumber, myToken);    //call get all di service
            }
            else
            {
                model = await _course.GetByName(name, myToken); //call get by name di service
            }

         

            return View(model); //return to view
        }
        public async Task<IActionResult> Delete(int id)
        {

            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            try
            {
                bool IsDelete = await _course.Delete(id, myToken); //memanggil metodh delete pada service
                if (IsDelete)
                {
                    TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mendelete data course id: {id}</div>";
                }
                return RedirectToAction("Index", new { pageNumber = 1 });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            try
            {
                var result = await _course.Insert(course, myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data course {result.Title}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }

        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await _course.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Course course)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            try
            {
                var result = await _course.Update(course, myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengupdate data course {result.Title}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public async Task<IActionResult> Detail(int id)
        {
            string myToken = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            IEnumerable<Enrollment> model = await _course.GetEnrollmentByCourseId(id, myToken);

            return View(model); //return to view
        }

    }
}
