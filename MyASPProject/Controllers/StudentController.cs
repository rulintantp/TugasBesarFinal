using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.Services;

namespace MyASPProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _student;

        public StudentController(IStudent student)
        {
            _student = student;
        }
        public async Task<IActionResult> Index(int pageNumber, string? name)
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];

            string myToken = string.Empty;

            IEnumerable<Student> model;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            if (name == null)
            {
                model = await _student.GetAll(pageNumber, myToken);    //call get all di service
            }
            else
            {
                model = await _student.GetByName(name, myToken);
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
                bool IsDelete = await _student.Delete(id, myToken);
                if (IsDelete)
                {
                    TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mendelete data student id: {id}</div>";
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
        public async Task<IActionResult> Create(Student student)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            try
            {
                var result = await _student.Insert(student, myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data student {result.FirstMidName} {result.LastName}</div>";
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
            var model = await _student.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token"); //Menampung Token
            }
            try
            {
                var result = await _student.Update(student, myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengupdate data student {result.FirstMidName} {result.LastName}</div>";
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
            IEnumerable<Enrollment> model = await _student.GetEnrollmentByID(id, myToken);

            return View(model); //return to view
        }
    }
}
