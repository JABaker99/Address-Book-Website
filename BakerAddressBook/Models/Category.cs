using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BakerAddressBook.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IList<Contact> Contacts { get; set; }
    }
}