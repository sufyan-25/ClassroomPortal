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
        //Topic Create Form View
        public IActionResult Create() => PartialView("_PartialTopicForm");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course,Topic topic)
        {
            if (HasNullProperties(course))
            {
                TempData["Success"] = "Topics added successfully";
                _context.Topics.Add(topic);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            if (HasNullProperties(topic))
            {
                TempData["Success"] = "Course added successfully";
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Please fill the fields";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(int id,Course course)
        {
            return Json(new {success=true,message="Data Received"});
        }
        private bool HasNullProperties(object obj)
        {
            if (obj == null)
                return true;

            var properties = obj.GetType().GetProperties().Where(p=>!p.GetMethod.IsVirtual);
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                if (value == null)
                    return true;
            }
            return false;
        }
    }
}
