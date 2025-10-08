using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using PlantsCatalog.Services;
using PlantsCatalog.Models;

namespace PlantsCatalog.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IPlantService _service;

        public CatalogController(IPlantService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(string? category, string? search, int page = 1, int pageSize = 6)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize <= 0 ? 6 : pageSize;

            var cat = category?.Trim();
            var term = search?.Trim();

            IEnumerable<Plant> all = _service.GetAllPlants();

            if (!string.IsNullOrWhiteSpace(cat))
            {
                all = all.Where(p =>
                    !string.IsNullOrEmpty(p.Category) &&
                    p.Category.Equals(cat, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(term))
            {
                all = all.Where(p =>
                    (!string.IsNullOrEmpty(p.Name) &&
                        p.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase)) ||
                    (!string.IsNullOrEmpty(p.Description) &&
                        p.Description.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
            }

            var total = all.Count();

            var maxPage = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            if (page > maxPage) page = maxPage;

            var items = all
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new CatalogViewModel
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Q = term
            };

            ViewBag.Category = cat;
            ViewBag.Categories = _service.GetAllPlants()
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .OrderBy(c => c)
                .ToList();

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var _plant = _service.GetById(id);
            if (_plant == null)
            {
                Response.StatusCode = 404;
                return View("NotFound", id);
            }
            return View(_plant);
        }
    }
}
