using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using SymphonyLimited.Models;

namespace SymphonyLimited.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly SymContext _context;
        public ContactController(SymContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var con = await _context.Contacts.ToListAsync();
            return PartialView("_PartialContactList", con);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact con)
        {
            if (!ModelState.IsValid)
            {
                return View(con);
            }

            if (con == null)
            {
                return View(con);

            }
            _context.Contacts.Add(con);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(int id, String Branch, String PTCL, String Whatsapp, String Location)
        {
            string result = "";
            var con = _context.Contacts.FirstOrDefault(x => x.BranchID == id);
            if (id == null || con == null)
            {
                result = "Data is null here";
                return Json(result);
            }
            if (Branch == null || PTCL == null || Whatsapp == null || Location == null)
            {
                result= "Null";
                return Json(result);
            }
                con.BranchName = Branch;
            con.PTCL = PTCL;
            con.Whatsapp = Whatsapp;
            con.Location = Location;
            _context.Contacts.Update(con);
            _context.SaveChanges();
            result = "Data Updated Successfully";
            return Json(result);
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(int? id)
        {
            string result = "";
            if (id == null)
            {
                result = "Id is Null";
                return Json(result);
            }
            var con = _context.Contacts.Where(x => x.BranchID == id).FirstOrDefault();
            if (con == null)
            {
                result = "Record not Found";
                return Json(result);
            }
            _context.Contacts.Remove(con);
            _context.SaveChanges();
            result = "Successfully Deleted";
            return Json(result);

        }
    }
}
