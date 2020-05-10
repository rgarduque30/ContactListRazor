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
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Contact Contact { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Contact = new Contact();
            if (id == null)
            {
                return Page();
            }
            Contact = await _db.Contact.FirstOrDefaultAsync(m => m.ID == id);
            if (Contact == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Contact.ID == 0)
                {
                    _db.Contact.Add(Contact);
                }
                else
                {
                    _db.Contact.Update(Contact);
                }
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