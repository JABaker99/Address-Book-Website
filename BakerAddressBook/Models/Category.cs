using System.ComponentModel.DataAnnotations;

/// <summary>
/// Baker Address Book - Category Model
/// Author: Jacob Baker
/// Created: 2025-09-21
/// Description:
/// Represents a contact category in the Baker Address Book application.
/// Each category can have multiple contacts associated with it.
/// </summary>
namespace BakerAddressBook.Models
{
    /// <summary>
    /// Represents a category for contacts.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Primary key for the category.
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// Name of the category. Required and limited to 50 characters.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property representing all contacts in this category.
        /// Can be null if no contacts exist yet.
        /// </summary>
        public IList<Contact>? Contacts { get; set; }
    }
}