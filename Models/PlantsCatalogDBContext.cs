using Microsoft.EntityFrameworkCore;
using PlantsCatalog.Models;

namespace PlantsCatalog.Models
{
    public class PlantsCatalogDBContext : DbContext
    {
        public PlantsCatalogDBContext(DbContextOptions<PlantsCatalogDBContext> options) : base(options) { }

        public DbSet<Plant> Plants { get; set; }

    }
}
