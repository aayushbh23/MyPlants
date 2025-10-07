using PlantsCatalog.Models;

namespace PlantsCatalog.Services
{
    public class InMemoryPlantService : IPlantService
    {
        private readonly List<Plant> _plants;

        public InMemoryPlantService(IWebHostEnvironment env)
        {
            _plants = new List<Plant>
            {
                new Plant { Id = 1, Name = "Monstera Deliciosa", Price = 30, Description = "Iconic split leaves. Easy indoor plant.", ImageFile = "monstera.png" },
                new Plant { Id = 2, Name = "Snake Plant (Sansevieria)", Price = 20, Description = "Hardy, low-light tolerant.", ImageFile = "snake.png" },
                new Plant { Id = 3, Name = "Fiddle Leaf Fig", Price = 40, Description = "Statement plant with violin-shaped leaves.", ImageFile = "fiddleleaf.png" },
                new Plant { Id = 4, Name = "ZZ Plant (Zamioculcas)", Price = 25, Description = "Glossy leaves, drought tolerant.", ImageFile = "zzplant.png" },
                new Plant { Id = 5, Name = "Pothos Golden", Price = 15, Description = "Fast-growing trailing plant.", ImageFile = "pothos.png" },
                new Plant { Id = 6, Name = "Peace Lily", Price = 23, Description = "Elegant white blooms, air-purifying.", ImageFile = "peacelily.png" },
                new Plant { Id = 7, Name = "Spider Plant", Price = 13, Description = "Easy-care, produces baby spiderettes.", ImageFile = "spider.png" },
                new Plant { Id = 8, Name = "Rubber Plant", Price = 28, Description = "Thick glossy leaves, bright light.", ImageFile = "rubber.png" },
                new Plant { Id = 9, Name = "Aloe Vera", Price = 16, Description = "Succulent with soothing gel.", ImageFile = "aloe.png" },
                new Plant { Id = 10, Name = "Bird of Paradise", Price = 45, Description = "Tropical look, large leaves.", ImageFile = "birdofparadise.png" },
                new Plant { Id = 11, Name = "Calathea Orbifolia", Price = 35, Description = "Striking patterns, prefers humidity.", ImageFile = "calathea.png" },
                new Plant { Id = 12, Name = "Chinese Money Plant", Price = 19, Description = "Round leaves, compact growth.", ImageFile = "moneyplant.png" }
            };
        }

        public IEnumerable<Plant> GetAll() => _plants;
    }
}
