using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;
using SymphonyLimited.ViewModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace SymphonyLimited.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        private readonly SymContext _context;

        public AboutController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var about = await _context.Abouts.Include(x=>x.Gallery).ToListAsync();
            return PartialView("_PartialAboutList", about);
        }

        //Create Function///////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Abouts.Add(about);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Data Successfully Added";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Data is null";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Something went wrong";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(int id,About about)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = error });
                }
                if (id == 0)
                    return Json(new { success = false, message = "Id is null" });

                var ab = await _context.Abouts.FirstOrDefaultAsync(x => x.AboutID == id);

                ab.title = about.title;
                ab.desc = about.desc;
                ab.GalleryID = about.GalleryID;
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Data Updated" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error happen" });
            }
        }
        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new { success = false, message = "Id is null" });
            }
            var about = _context.Abouts.FirstOrDefault(x => x.AboutID == id);
            _context.Abouts.Remove(about);
            _context.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
    }
}
