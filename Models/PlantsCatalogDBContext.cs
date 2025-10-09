using Microsoft.EntityFrameworkCore;

namespace PlantsCatalog.Models
{
    // Database context for the Plants Catalog application
    public class PlantsCatalogDBContext : DbContext
    {
        public PlantsCatalogDBContext(DbContextOptions<PlantsCatalogDBContext> options) : base(options) { }

        public DbSet<Plant> Plants { get; set; } // Table mapping for Plant entities
    }
}
