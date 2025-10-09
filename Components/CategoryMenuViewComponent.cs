using Microsoft.AspNetCore.Mvc;
using PlantsCatalog.Services;

namespace PlantsCatalog.ViewComponents
{
    // Renders a category dropdown/menu for filtering plants
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly IPlantService _plantService;

        public CategoryMenuViewComponent(IPlantService plantService)
        {
            _plantService = plantService;
        }

        public IViewComponentResult Invoke(string? selected)
        {
            // Fetch distinct, alphabetically sorted category names
            var categories = _plantService.GetAllPlants()
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .OrderBy(c => c)
                .ToList();

            // Mark which category is currently active
            ViewBag.SelectedCategory = selected;

            return View(categories);
        }
    }
}
