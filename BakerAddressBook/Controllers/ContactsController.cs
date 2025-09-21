using BakerAddressBook.Data;
using BakerAddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Baker Address Book - Contacts Controller
/// Author: Jacob Baker
/// Created: 2025-09-21
/// Description:
/// Controller responsible for managing CRUD operations for contacts in the Baker Address Book application.
/// Provides actions to view details, create, edit, and delete contacts, including validation and category selection.
/// </summary>
namespace BakerAddressBook.Controllers
{
    /// <summary>
    /// Controller for managing contacts (CRUD operations).
    /// </summary>
    public class ContactsController : Controller
    {
        /// <summary>
        /// Application database context.
        /// </summary>
        private readonly BakerAppDbContext _context;

        /// <summary>
        /// Constructor that receives the DbContext via dependency injection.
        /// </summary>
        /// <param name="context">The BakerAppDbContext instance.</param>
        public ContactsController(BakerAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        // /// Displays the details of a single contact.
        // /// </summary>
        // /// <param name="id">The ID of the contact to display.</param>
        // /// <returns>View with contact details or NotFound if the contact does not exist.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contact = await _context.Contacts
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ContactId == id);

            if (contact == null) return NotFound();

            return View(contact);
        }

        /// <summary>
        // /// Displays the confirmation page for deleting a contact.
        // /// </summary>
        // /// <param name="id">The ID of the contact to delete.</param>
        // /// <returns>View for confirmation or NotFound if the contact does not exist.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Contact? contact = await _context.Contacts.Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ContactId == id.Value);
            if (contact == null) return NotFound();

            return View(contact);
        }

        /// <summary>
        // /// Handles POST request to delete a contact after confirmation.
        // /// </summary>
        // /// <param name="id">The ID of the contact to delete.</param>
        // /// <returns>Redirects to Home/Index after deletion.</returns>
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

        /// <summary>
        /// Displays the form to create a new contact.
        /// </summary>
        /// <returns>View with an empty Contact model and category list.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.IsEdit = false;
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View("CreateEdit", new Contact());
        }

        /// <summary>
        /// Handles POST request to create a new contact.
        /// </summary>
        /// <param name="contact">The Contact object submitted from the form.</param>
        /// <returns>Redirects to Details page if successful, otherwise redisplays form with errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
                return View("CreateEdit", contact);
            }

            contact.DateCreated = DateTime.UtcNow;

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = contact.ContactId });
        }

        /// <summary>
        /// Displays the form to edit an existing contact.
        /// </summary>
        /// <param name="id">The ID of the contact to edit.</param>
        /// <returns>View with contact data or NotFound if the contact does not exist.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Contact? contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            ViewBag.IsEdit = true;
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();

            return View("CreateEdit", contact);
        }

        /// <summary>
        /// Handles POST request to save edits to an existing contact.
        /// </summary>
        /// <param name="id">The ID of the contact being edited.</param>
        /// <param name="contact">The updated Contact object from the form.</param>
        /// <returns>Redirects to Details if successful, otherwise redisplays the form with errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.ContactId) return NotFound();

            if (ModelState.IsValid)
            {
                Contact? existingContact = await _context.Contacts.FindAsync(id);
                if (existingContact == null) return NotFound();

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
