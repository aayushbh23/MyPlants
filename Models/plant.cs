using System.ComponentModel.DataAnnotations.Schema;

namespace PlantsCatalog.Models
{
    // Represents a plant product in the catalog
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")] // Ensures consistent price precision in DB
        public decimal Price { get; set; }

        public string ImageFile { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
