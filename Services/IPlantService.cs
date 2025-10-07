using PlantsCatalog.Models;

namespace PlantsCatalog.Services
{
    public interface IPlantService
    {
        IEnumerable<Plant> GetAllPlants(string? category = null, string? search = null);
        Plant? GetById(int id);
    }
}
