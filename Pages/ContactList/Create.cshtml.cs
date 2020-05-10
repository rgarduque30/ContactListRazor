using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactListRazor.Pages.ContactList
{
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Contact Contact { get; set; }
        public async Task OnGet(int id)
        {
            Contact = await _db.Contact.FindAsync(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Contact.AddRange(Contact);
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