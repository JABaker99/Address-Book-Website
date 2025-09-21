using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Baker Address Book - Contact Model
/// Author: Jacob Baker
/// Created: 2025-09-21
/// Description:
/// Represents a contact in the Baker Address Book application.
/// Includes personal information, phone number, category, and creation date.
/// </summary>
namespace BakerAddressBook.Models
{
    /// <summary>
    /// Represents a single contact in the address book.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Primary key for the contact.
        /// </summary>
        [Key]
        public int ContactId { get; set; }

        /// <summary>
        /// Contact's first name. Required, max length 50 characters.
        /// </summary>
        [Required(ErrorMessage = "First name required.")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Contact's last name. Required, max length 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Last name required.")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Optional nickname for the contact. Max length 50 characters.
        /// </summary>
        [StringLength(50)]
        public string? Nickname { get; set; }

        /// <summary>
        /// Contact's phone number. Required and validated as a phone number.
        /// </summary>
        [Required(ErrorMessage = "Phone number required.")]
        [Phone(ErrorMessage = "Enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key for the contact's category. Required.
        /// Defaults to 1 if not specified.
        /// </summary>
        [Required(ErrorMessage = "Category required.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; } = 1;

        /// <summary>
        /// Navigation property to the contact's category.
        /// Allows accessing category details via EF Core.
        /// </summary>
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        /// <summary>
        /// Date the contact was created.
        /// Defaults to the current date/time when a new contact is created.
        /// </summary>
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}