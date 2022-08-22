using Microsoft.AspNetCore.Mvc;

namespace MyASPProject.ViewComponents
{
    public class HelloViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string model = "Hello View Component";
            return View("Default", model);
        }
    }
}
