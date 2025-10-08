using PlantsCatalog.Models;
using PlantsCatalog.Services;
using Microsoft.EntityFrameworkCore;

namespace PlantsCatalog.Services
{
    public class EfPlantService : IPlantService
    {
        private readonly PlantsCatalogDBContext _db;
        public EfPlantService(PlantsCatalogDBContext context)
        {
            _db = context;
        }

        public IEnumerable<Plant> GetAllPlants(string? category = null, string? search = null)
        {
            IQueryable<Plant> query = _db.Plants;

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            return query.ToList();
        }

        public Plant? GetById(int id)
        {
            return _db.Plants.Find(id);
        }
    }
}
