// Jacob Baker — started 2025-09-20
using Microsoft.EntityFrameworkCore;
using BakerAddressBook.Models;

/// <summary>
/// Baker Address Book - Database Context
/// Author: Jacob Baker
/// Started: 2025-09-20
/// Description:
/// Represents the application's database context for Entity Framework Core.
/// Handles access to Contacts and Categories tables and seeds initial data.
/// </summary>
namespace BakerAddressBook.Data
{
    /// <summary>
    /// EF Core DbContext for the Baker Address Book application.
    /// </summary>
    public class BakerAppDbContext : DbContext
    {
        /// <summary>
        /// Constructor accepting DbContext options, passed by dependency injection.
        /// </summary>
        /// <param name="options">Options for configuring the DbContext.</param>
        public BakerAppDbContext(DbContextOptions<BakerAppDbContext> options) : base(options) { }

        /// <summary>
        /// Represents the Contacts table in the database.
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Represents the Categories table in the database.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Configures the model and seeds initial data for the database.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder used to configure entities.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Family" },
                new Category { CategoryId = 2, Name = "Friend" },
                new Category { CategoryId = 3, Name = "Work" }
            );

            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Alice",
                    LastName = "Kelly",
                    Nickname = "Ally",
                    PhoneNumber = "123-456-7890",
                    CategoryId = 2,
                    DateCreated = new DateTime(2025, 1, 10)
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Bob",
                    LastName = "Baker",
                    Nickname = null,
                    PhoneNumber = "770-423-6789 ",
                    CategoryId = 1,
                    DateCreated = new DateTime(2025, 2, 11)
                },
                new Contact
                {
                    ContactId = 3,
                    FirstName = "Charlie",
                    LastName = "Baker",
                    Nickname = "Chuck",
                    PhoneNumber = "476-543-2211",
                    CategoryId = 3,
                    DateCreated = new DateTime(2025, 3, 12)
                }
            );
        }
    }
}
