using PlantsCatalog.Models;

namespace PlantsCatalog.Services
{
    public interface IPlantService
    {
        IEnumerable<Plant> GetAll();
    }
}