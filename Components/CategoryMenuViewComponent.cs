// using Microsoft.AspNetCore.Mvc;
// using PlantsCatalog.Services;

// namespace PlantsCatalog.Components
// {
//     public class CategoryMenuViewComponent : ViewComponent
//     {
//         private readonly IPlantService _plantService;
//         public CategoryMenuViewComponent(IPlantService plantService)
//         {
//             _plantService = plantService;
//         }

//         public IViewComponentResult Invoke()
//         {
//             // Get distinct categories from all products
//             var categories = _plantService.GetAllPlants()
//                                          .Select(p => p.Category)
//                                          .Where(cat => !string.IsNullOrEmpty(cat))
//                                          .Distinct()
//                                          .OrderBy(cat => cat);
//             return View(categories);
//         }
//     }
// }


using Microsoft.AspNetCore.Mvc;
using PlantsCatalog.Services;

namespace PlantsCatalog.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly IPlantService _plantService;

        public CategoryMenuViewComponent(IPlantService plantService)
        {
            _plantService = plantService;
        }

        // Accepts an optional 'selected' parameter from the parent view
        public IViewComponentResult Invoke(string? selected)
        {
            var categories = _plantService.GetAllPlants()
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .OrderBy(c => c)
                .ToList();

            ViewBag.SelectedCategory = selected;
            return View(categories);
        }
    }
}

