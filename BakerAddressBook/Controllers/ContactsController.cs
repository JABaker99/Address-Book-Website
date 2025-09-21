using BakerAddressBook.Data;
using BakerAddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

            Contact? contact = await _context.Contacts.Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ContactId == id.Value);
            if (contact == null) return NotFound();

            return View(contact);
        }

        // POST: /contacts/delete/5/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Contact? contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        // =======================
        // CREATE
        // =======================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.IsEdit = false;
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View("CreateEdit", new Contact()); // new empty contact
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            Console.WriteLine("➡️ POST Create triggered");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ Validation failed");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"   - {state.Key}: {error.ErrorMessage}");
                    }
                }

                ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
                return View("CreateEdit", contact);
            }

            // ✅ assign DateCreated automatically
            contact.DateCreated = DateTime.UtcNow;

            Console.WriteLine("✅ Adding contact:");
            Console.WriteLine($"   Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($"   Phone: {contact.PhoneNumber}");
            Console.WriteLine($"   CategoryId: {contact.CategoryId}");
            Console.WriteLine($"   DateCreated: {contact.DateCreated}");

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            Console.WriteLine($"✅ Saved with ID: {contact.ContactId}");

            return RedirectToAction(nameof(Details), new { id = contact.ContactId });
        }



        // =======================
        // EDIT
        // =======================
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            ViewBag.IsEdit = true;
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();

            return View("CreateEdit", contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.ContactId) return NotFound();

            if (ModelState.IsValid)
            {
                var existingContact = await _context.Contacts.FindAsync(id);
                if (existingContact == null) return NotFound();

                // Update only allowed fields (not DateCreated)
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Nickname = contact.Nickname;
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.CategoryId = contact.CategoryId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = existingContact.ContactId });
            }

            ViewBag.IsEdit = true;
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View("CreateEdit", contact);
        }
    }
}
