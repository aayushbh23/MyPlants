using PlantsCatalog.Models;

namespace PlantsCatalog.Services
{
    // Handles plant data operations through Entity Framework
    public class EfPlantService : IPlantService
    {
        private readonly PlantsCatalogDBContext _db;

        public EfPlantService(PlantsCatalogDBContext context) => _db = context;

        public IEnumerable<Plant> GetAllPlants(string? category = null, string? search = null)
        {
            var query = _db.Plants.AsQueryable();

            // Optional filtering by category or search term
            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p =>
                    p.Name.Contains(search) || p.Description.Contains(search));

            return query.ToList();
        }

        public Plant? GetById(int id) => _db.Plants.Find(id);
    }
}
