using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;
using System.Diagnostics;

namespace SymphonyLimited.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SymContext _context;
        public HomeController(ILogger<HomeController> logger, SymContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(x => x.Topics).ThenInclude(t => t.Gallery).ToListAsync();
            ViewBag.Courses = courses;
            return View();
        }
        public async Task<IActionResult> About()
        {
            var about = await _context.Abouts.ToListAsync();
            return View(about);
        }
        public IActionResult Contact() {
            var con = _context.Contacts.ToList();
            return View(con);
        }
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            var faq = _context.FAQs.ToList();

            return View(faq);
        }
        public IActionResult CourseDetail() {
            return View();
        }
        public async Task<IActionResult> ExamDetail() {
            var exam = await _context.Exams.Include(x=>x.Gallery).ToListAsync();
            return View(exam);
        }
        [HttpPost]
        public IActionResult ResultSearch(string id)
        {
            var result = _context.Results.Include(x=>x.Student).FirstOrDefault(x => x.StudentID == id);
            if (result==null)
            {
                TempData["Error"] = "StudentID does not exist / Result not yet announced";
                return RedirectToAction(nameof(ExamDetail));
            }
            return View(result);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{

        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [Route("Home/Error")]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionFeature != null)
            {
                var exception = exceptionFeature.Error;
                Console.WriteLine($"Error: {exception.Message}");
            }
            return View("Error");
        }
        public IActionResult TestError()
        {
            throw new Exception("Test exception handling");
        }

    }
}
