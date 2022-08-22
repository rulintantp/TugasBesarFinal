using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.Services;

namespace MyASPProject.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollment _enrollment;

        public EnrollmentController(IEnrollment enrollment)
        {
            _enrollment = enrollment;
        }
        public async Task<IActionResult> Index(int pageNumber, string? name)
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];

            string myToken = string.Empty;

            IEnumerable<Enrollment> model;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            if (name == null)
            {
                model = await _enrollment.GetAll(pageNumber, myToken);    //call get all di service
            }
            else
            {
                model = await _enrollment.GetByName(name, myToken);
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
                bool IsDelete = await _enrollment.Delete(id, myToken);
                if (IsDelete)
                {
                    TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mendelete data enrollment id: {id}</div>";
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
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            try
            {
                var result = await _enrollment.Insert(enrollment, myToken);
                if (result.EnrollmentID > 0)
                {
                    TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data enrollment {result.EnrollmentID}</div>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Data dengan ID Student atau ID Course tidak ditemukan</div>";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }

        }
    }
}
