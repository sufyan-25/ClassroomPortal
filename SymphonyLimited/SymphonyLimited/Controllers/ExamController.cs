using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;

namespace SymphonyLimited.Controllers
{
    public class ExamController : Controller
    {
        private readonly SymContext _context;
        public ExamController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List() {
            var exam = _context.Exams.ToList();
            return PartialView("_PartialExamList", exam);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exam exam)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Model Error";
                    return RedirectToAction(nameof(Index));
                }
                if (exam == null)
                {
                    TempData["Error"] = "Fill all the fields";
                    return RedirectToAction(nameof(Index));
                }
                _context.Exams.Add(exam);
                _context.SaveChanges();
                TempData["Success"] = "Data added successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Something went wrong!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
