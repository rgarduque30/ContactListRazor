using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactListRazor.Pages.ContactList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Contact Contact { get; set; }
        public async Task OnGet(int id)
        {
            Contact = await _db.Contact.FindAsync(id);
        }
        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var contact = await _db.Contact.FindAsync(id);
                contact.ID = Contact.ID;
                contact.FirstName = Contact.FirstName;
                contact.MiddleName = Contact.MiddleName;
                contact.LastName = Contact.LastName;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else 
            {
                return Page();
            }
        }
    }
}