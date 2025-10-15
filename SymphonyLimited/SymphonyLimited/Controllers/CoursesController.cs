using Microsoft.AspNetCore.Mvc;

namespace SymphonyLimited.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
