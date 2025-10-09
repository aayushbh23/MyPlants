using PlantsCatalog.Models;

namespace PlantsCatalog.Services
{
    // Defines data access operations for plants
    public interface IPlantService
    {
        IEnumerable<Plant> GetAllPlants(string? category = null, string? search = null); // Retrieve all or filtered plants
        Plant? GetById(int id);                                                         // Fetch a single plant by ID
    }
}
