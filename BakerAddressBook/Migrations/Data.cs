// Jacob Baker — started 2025-09-20
using Microsoft.EntityFrameworkCore;
using BakerAddressBook.Models;
using System;

namespace BakerAddressBook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

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
