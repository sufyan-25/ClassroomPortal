using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;
using SymphonyLimited.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;

namespace SymphonyLimited.Controllers
{
    public class AuthController : Controller
    {
        private readonly SymContext _context;
        public AuthController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginView lgv)
        {
            if (!ModelState.IsValid) return View(lgv);
            if (lgv == null) return View(lgv);
            var auth = await _context.Auths.Where(x => x.Username == lgv.Username && x.Password == lgv.Password).FirstOrDefaultAsync();
            if (auth == null)
            {
                ViewBag.Message = "Invalid Credentials";
                return View(lgv);
            }
            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(auth.AdminID)),
                new Claim(ClaimTypes.Name, auth.Name),
            };
            var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal, new AuthenticationProperties()
                { IsPersistent = lgv.RememberMe });

            return RedirectToAction(nameof(Dashboard));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                );
            return LocalRedirect("/");
        }
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var stCount = await _context.Students.Include(x => x.Results).GroupBy(s => s.Status).Select(g => new { Status = g.Key, Count = g.Count()}).ToListAsync();
            var courseCount = _context.Courses.Count();
            var topicCount = _context.Topics.Count();
            var branches = _context.Contacts.ToList();

            ViewBag.StCount = stCount;
            ViewBag.CourseCount = courseCount;
            ViewBag.TopicCount = topicCount;
            ViewBag.Branches = branches;
            return View();
        }
        public IActionResult Forbidden()
        {

            return LocalRedirect("/");
        }

    }
}
