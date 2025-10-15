using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;
using SymphonyLimited.ViewModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace SymphonyLimited.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly SymContext _context;
        public StudentController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var st = await _context.Students.ToListAsync();
            return PartialView("_PartialStudentList", st);
        }

        //Create Function///////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentView sv)
        {
            try
            {

                //Check Image file
                if (sv.ImageFile == null)
                {
                    TempData["Error"] = "Please select Image";
                    return RedirectToAction(nameof(Index));
                }
                if (sv.ImageFile.Length > 1000 * 1024)
                {
                    TempData["Error"] = "Selected image should be under 1MB";
                    return RedirectToAction(nameof(Index));
                }

                var thumb = await ProcessingImage(sv.ImageFile);

                Student student = new()
                {
                    StudentID = GenCustomId(),
                    Name = sv.Name,
                    Email = sv.Email,
                    Phone = sv.Phone,
                    Address = sv.Address,
                    ThumbPic = thumb,
                    Status = "Entrance"
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Data Successfully Added";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //Generate Custom ID
        private string GenCustomId()
        {
            Random random = new Random();
            string newId;
            do
            {
                int randomPart = random.Next(100000, 999999);
                newId = randomPart + DateTime.Now.ToString("MMyy");
            }
            while (_context.Students.Any(s => s.StudentID == newId));
            return newId;
        }

        //Image Processing Functions
        private async Task<string> ProcessingImage(IFormFile imageFile)
        {
            using var ms = new MemoryStream();
            await imageFile.CopyToAsync(ms);
            var bytes = ms.ToArray();
            var thumbBytes = ResizeImage(bytes, 300, 300);
            var thumb = Convert.ToBase64String(thumbBytes);
            return thumb;
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
