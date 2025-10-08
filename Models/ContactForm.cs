using System.ComponentModel.DataAnnotations;

namespace PlantsCatalog.Models
{
    public class ContactForm
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(1000)]
        public string Message { get; set; } = string.Empty;
    }
}
