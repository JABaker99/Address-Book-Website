using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakerAddressBook.Data;
using BakerAddressBook.Models;

namespace BakerAddressBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly BakerAppDbContext _context;

        public ContactsController(BakerAppDbContext context)
        {
            _context = context;
        }

        // GET: /contacts/details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contact = await _context.Contacts
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ContactId == id);

            if (contact == null) return NotFound();

            return View(contact);
        }

        // GET: /contacts/delete/5/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Contact contact = await _context.Contacts.Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ContactId == id.Value);
            if (contact == null) return NotFound();

            return View(contact);
        }

        // POST: /contacts/delete/5/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Contact contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
