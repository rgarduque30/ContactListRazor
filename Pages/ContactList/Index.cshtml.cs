using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactListRazor.Pages.ContactList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Contact> Contacts { get; set; }
        public async Task OnGet()
        {
            Contacts = await _db.Contact.ToListAsync();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var contact = await _db.Contact.FindAsync(id);
            if (contact == null) 
            {
                return NotFound();
            }
            _db.Remove(contact);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}