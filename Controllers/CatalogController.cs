using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PlantsCatalog.Services;
using PlantsCatalog.Models;

namespace PlantsCatalog.Controllers
{
    // Catalog browsing: list, filter, search, paginate and details
    public class CatalogController : Controller
    {
        private readonly IPlantService _service;

        public CatalogController(IPlantService service) => _service = service;

        [HttpGet]
        public IActionResult Index(string? category, string? search, int page = 1, int pageSize = 6)
        {
            // Guard inputs to sane defaults
            page = page < 1 ? 1 : page;
            pageSize = pageSize <= 0 ? 6 : pageSize;

            var cat  = category?.Trim();
            var term = search?.Trim();

            // Load once to avoid multiple service hits
            var allPlants = _service.GetAllPlants().ToList();
            IEnumerable<Plant> query = allPlants;

            // Case-insensitive category match
            if (!string.IsNullOrWhiteSpace(cat))
            {
                query = query.Where(p =>
                    !string.IsNullOrEmpty(p.Category) &&
                    p.Category.Equals(cat, StringComparison.InvariantCultureIgnoreCase));
            }

            // Case-insensitive search in name/description
            if (!string.IsNullOrWhiteSpace(term))
            {
                query = query.Where(p =>
                    (!string.IsNullOrEmpty(p.Name) && p.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase)) ||
                    (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
            }

            var total   = query.Count();
            var maxPage = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            if (page > maxPage) page = maxPage;

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new CatalogViewModel
            {
                Items      = items,
                Page       = page,
                PageSize   = pageSize,
                TotalCount = total,
                Q          = term
            };

            // Sidebar data: distinct full category list (not filtered)
            ViewBag.Category   = cat;
            ViewBag.Categories = allPlants
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .OrderBy(c => c)
                .ToList();

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var plant = _service.GetById(id);

            // Keep custom 404 view while setting the correct status code
            if (plant == null)
            {
                Response.StatusCode = 404;
                return View("NotFound", id);
            }

            return View(plant);
        }
    }
}
