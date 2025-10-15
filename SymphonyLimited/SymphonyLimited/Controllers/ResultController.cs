using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SymphonyLimited.Models;

namespace SymphonyLimited.Controllers
{
    [Authorize]
    public class ResultController : Controller
    {
        private readonly SymContext _context;
        public ResultController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            var exams = _context.Exams.Select(s => new { s.Id, s.Title }).ToList();
            ViewBag.ExamList = new SelectList(exams, "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Result result)
        {
            ModelState.Remove("Status");
            result.Status = result.Marks < 40 ? "Failed" : "Passed";

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
                var debug = string.Join(" | ", errors);
                TempData["Error"] = debug;
                ViewBag.ExamList = new SelectList(_context.Exams, "Id", "Title", result.ExamID);
                return View();
            }
            TempData["Success"] = "Result Updated";
            var students = _context.Students.FirstOrDefault(x => x.StudentID == result.StudentID);
            if (students != null) { students.Status = result.Status; }
            _context.Results.Add(result);
            _context.SaveChanges();
            return RedirectToAction(nameof(Create));
        }
        public IActionResult List()
        {
            var students = _context.Students.Where(x => x.Status == "Entrance").ToList();
            return PartialView("_PartialResult", students);
        }
    }
}
