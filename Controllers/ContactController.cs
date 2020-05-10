using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListRazor.Controllers
{
    [Route("api/Contact")]
    [ApiController]
    public class ContactController : Controller
    {
        private ApplicationDbContext _db;
        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Contact.ToListAsync()});
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _db.Contact.FindAsync(id);
            if (contact == null)
            {
                return Json(new { success=false, message="An error occured." });
            }
            _db.Remove(contact);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Record deleted!" });
        }
    }
}