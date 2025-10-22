using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var courses = await _context.Courses.Include(g=>g.Gallery)
                .Include(x => x.Topics)
                .ThenInclude(t=>t.Gallery)
                .ToListAsync();
            return PartialView("_PartialCourseList",courses);
        }
        //Topic Create Form View
        public IActionResult Create()
        {
            var courses = _context.Courses.Select(s => new { s.Id, s.Name }).ToList();
            ViewBag.CourseList = new SelectList(courses, "Id", "Name");
            return PartialView("_PartialTopicForm");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Course added successfully";
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Please fill the fields";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateTopic(Topic topic)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Topic added successfully";
                _context.Topics.Add(topic);
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
    }
}
