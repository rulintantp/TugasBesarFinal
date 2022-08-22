using Microsoft.AspNetCore.Mvc;

namespace MyASPProject.Controllers
{
    public class StartbootstrapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
