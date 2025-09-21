using System.Diagnostics;
using BakerAddressBook.Data;
using BakerAddressBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakerAddressBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly BakerAppDbContext _context;

        public HomeController(BakerAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Contact> contacts = _context.Contacts
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToList();

            return View(contacts);
        }
    }
}
