using System.ComponentModel.DataAnnotations.Schema;

namespace PlantsCatalog.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string ImageFile { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
