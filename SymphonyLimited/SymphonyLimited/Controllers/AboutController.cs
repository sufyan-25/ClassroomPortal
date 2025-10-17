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
            var about = await _context.Abouts.ToListAsync();
            return PartialView("_PartialAboutList", about);
        }

        //Create Function///////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutView av)
        {
            try
            {
                //Check Image file
                if (av.ImageFile == null)
                {
                    TempData["Error"] = "Please select Image";
                    return RedirectToAction(nameof(Index));
                }
                if (av.ImageFile.Length > 1000 * 1024)
                {
                    TempData["Error"] = "Selected image should be under 1MB";
                    return RedirectToAction(nameof(Index));
                }

                var (base64, thumb) = await ProcessingImage(av.ImageFile);

                About about = new()
                {
                    title = av.title,
                    desc = av.desc,
                    base64 = base64,
                    thumb = thumb
                };

                _context.Abouts.Add(about);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Data Successfully Added";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(int id, string title, string desc, IFormFile? ImageFile)
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

                var about = await _context.Abouts.FirstOrDefaultAsync(x => x.AboutID == id);
                if (ImageFile != null)
                {
                    //return Json(new { success = false, message = "Image file is null" });
                    var (base64, thumb) = await ProcessingImage(ImageFile);
                    about.base64 = base64;
                    about.thumb = thumb;
                }

                about.title = title;
                about.desc = desc;

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


        private async Task<(String base64, String thumb)> ProcessingImage(IFormFile imageFile)
        {
            using var ms = new MemoryStream();
            await imageFile.CopyToAsync(ms);
            var bytes = ms.ToArray();
            var base64 = Convert.ToBase64String(bytes);
            var thumbBytes = ResizeImage(bytes, 150, 150);
            var thumb = Convert.ToBase64String(thumbBytes);
            return (base64, thumb);
        }

        private byte[] ResizeImage(byte[] imageBytes, int width, int height)
        {
            using var ms = new MemoryStream(imageBytes);
            using var image = Image.FromStream(ms);
            using var thumbnail = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(thumbnail);
            graphics.DrawImage(image, 0, 0, width, height);
            using var os = new MemoryStream();
            thumbnail.Save(os, ImageFormat.Jpeg);
            return os.ToArray(); ;
        }
    }
}
