using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;
using SymphonyLimited.ViewModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace SymphonyLimited.Controllers
{
    public class GalleryController : Controller
    {
        private readonly SymContext _context;
        public GalleryController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var gallery = await _context.Galleries.ToListAsync();
            return PartialView("_PartialGallery", gallery);
        }
        public async Task<IActionResult> SelectImage()
        {
            var gallery = await _context.Galleries.ToListAsync();
            return PartialView("_PartialSelectImage", gallery);
        }
        [HttpPost]
        public async Task<IActionResult> Create(GalleryView gv)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Model is not valid";
                return RedirectToAction(nameof(Index));
            }
            if (gv.ImageFile == null)
            {
                TempData["Error"] = "Image file is null";
                return RedirectToAction(nameof(Index));
            }
            if (gv.ImageFile.Length > 500 * 1024)
            {
                TempData["Error"] = "Image file should be under 500kb";
                return RedirectToAction(nameof(Index));
            }
            var (base64, thumb) = await ProcessingImage(gv.ImageFile);
            Gallery gallery = new()
            {
                AltText = gv.AltText,
                ImageBs64 = base64,
                Thumb64 = thumb
            };
            _context.Galleries.Add(gallery);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Data Successfully Added";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(int[] ids)
        {
            if (ids.Length == 0)
            {
                return Json(new { success = true, message = $" {ids.Count()} Ids found" });
            }
            var gallery = _context.Galleries.Where(g => ids.Contains(g.Id)).ToList();
            _context.Galleries.RemoveRange(gallery);
            _context.SaveChanges();
            return Json(new { success = true, message = $" {ids.Count()} Delete successfully" });
        }

        //Image processing Functions
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