using System.ComponentModel.DataAnnotations;

namespace PlantsCatalog.Models
{
    // Represents data submitted from the Contact page form
    public class ContactForm
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(1000)]
        public string Message { get; set; } = string.Empty; // User's message content
    }
}
