using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;

namespace SymphonyLimited.Controllers
{
    [Authorize]
    public class FAQController : Controller
    {
        private readonly SymContext _context;
        public FAQController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var faq = await _context.FAQs.ToListAsync();

            return PartialView("_PartialFaqList", faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FAQ faq)
        {
            if (!ModelState.IsValid)
            {
                return View(faq);
            }
            if (faq == null)
            {
                return View(faq);
            }
            _context.FAQs.Add(faq);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public JsonResult Edit(int id, String Question, String Answer)
        {
            if (id == null)
            {
                return Json(new { error = true, message = "ID is Null" });
            }
            var faq = _context.FAQs.FirstOrDefault(x => x.FAQID == id);
            faq.Question = Question;
            faq.Answer = Answer;
            _context.FAQs.Update(faq);
            _context.SaveChanges();

            return Json(new { success = true, message = "Data received"});
        }
        [HttpPost][ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return View(id);
            }
            var faq = _context.FAQs.FirstOrDefault(x => x.FAQID == id);
            _context.FAQs.Remove(faq);
            _context.SaveChanges();
            return Json(new { success = true, result = "Id got" });
        }
    }
}
