using BakerAddressBook.Data;
using BakerAddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BakerAddressBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly BakerAppDbContext _context;

        public HomeController(BakerAppDbContext context)
        {
            _context = context;
        }

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