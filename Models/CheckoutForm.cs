using System.ComponentModel.DataAnnotations;

namespace PlantsCatalog.Models
{
    public class CheckoutForm
    {
        // Contact
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Address
        [Required, StringLength(120)]
        public string AddressLine1 { get; set; } = string.Empty;

        [StringLength(120)]
        public string? AddressLine2 { get; set; }

        [Required, StringLength(60)]
        public string City { get; set; } = string.Empty;

        [Required, StringLength(60)]
        public string State { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string Postcode { get; set; } = string.Empty;

        [Required, StringLength(60)]
        public string Country { get; set; } = "Australia";

        // Payment (sample/demo only)
        [Required, StringLength(100)]
        public string CardholderName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{16}$")]
        public string CardNumber { get; set; } = string.Empty;

        [Required, Range(1, 12)]
        public int ExpiryMonth { get; set; }

        [Required, Range(2024, 2035)]
        public int ExpiryYear { get; set; }

        [Required, StringLength(4, MinimumLength = 3)]
        public string CVV { get; set; } = string.Empty;
    }
}
