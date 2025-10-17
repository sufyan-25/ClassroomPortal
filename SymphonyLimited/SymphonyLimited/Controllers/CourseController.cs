using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;

namespace SymphonyLimited.Controllers
{
    public class CourseController : Controller
    {
        private readonly SymContext _context;
        public CourseController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var courses = await _context.Courses.Include(x => x.Topics).ToListAsync();
            return PartialView("_PartialCourseList",courses);
        }
    }
}
