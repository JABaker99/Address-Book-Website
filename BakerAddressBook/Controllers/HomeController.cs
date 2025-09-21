using BakerAddressBook.Data;
using BakerAddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

/// <summary>
/// Baker Address Book - Home Controller
/// Author: Jacob Baker
/// Created: 2025-09-21
/// Description:
/// This controller handles the Home page of the Baker Address Book application.
/// It retrieves and displays a list of contacts ordered by last name and first name.
/// </summary>
namespace BakerAddressBook.Controllers
{
    /// <summary>
    /// Controller for the Home page of the application.
    /// Handles displaying all contacts.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Reference to the application's database context.
        /// </summary>
        private readonly BakerAppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public HomeController(BakerAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the Index action.
        /// Retrieves all contacts from the database, including their categories,
        /// orders them by LastName then FirstName, and passes them to the view.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation, with an IActionResult containing the view.</returns>
        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = await _context.Contacts
                .Include(c => c.Category)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync();

            return View(contacts);
        }
    }
}