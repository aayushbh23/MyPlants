// using Microsoft.AspNetCore.Mvc;
// using System.Globalization;
// using PlantsCatalog.Services;
// using PlantsCatalog.Models;

// namespace PlantsCatalog.Controllers
// {
//     public class CatalogController : Controller
//     {
//         private readonly IPlantService _service;

//         public CatalogController(IPlantService service)
//         {
//             _service = service;
//         }

//         public IActionResult Index(string? category, string? search, int page = 1, int pageSize = 6)
//         {
//             page = page < 1 ? 1 : page;
//             pageSize = pageSize <= 0 ? 6 : pageSize;

//             // var termRaw = string.IsNullOrWhiteSpace(search) ? q : search;
//             var term = search?.Trim();
//             var termLower = term?.ToLower(CultureInfo.InvariantCulture);

//             IEnumerable<Plant> all = _service.GetAllPlants(category, term);

//             // var all = _service.GetAll();

//             // if (!string.IsNullOrWhiteSpace(q))
//             // {
//             //     var ql = q.Trim().ToLowerInvariant();
//             //     all = all.Where(p =>
//             //         p.Name.ToLowerInvariant().Contains(ql) ||
//             //         p.Description.ToLowerInvariant().Contains(ql));
//             // }

//             if (!string.IsNullOrWhiteSpace(termLower))
//             {
//                 all = all.Where(p =>
//                     (!string.IsNullOrEmpty(p.Name) && p.Name.ToLower(CultureInfo.InvariantCulture).Contains(termLower)) ||
//                     (!string.IsNullOrEmpty(p.Description) && p.Description.ToLower(CultureInfo.InvariantCulture).Contains(termLower)));
//             }

//             var total = all.Count();
//             var items = all
//                 .Skip((page - 1) * pageSize)
//                 .Take(pageSize)
//                 .ToList();

//             var vm = new CatalogViewModel
//             {
//                 Items = items,
//                 Page = page,
//                 PageSize = pageSize,
//                 TotalCount = total,
//                 Q = term
//                 // Q = q
//             };

//             return View(vm);
//         }
//     }
// }


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
            // Normalize inputs
            page = page < 1 ? 1 : page;
            pageSize = pageSize <= 0 ? 6 : pageSize;

            var cat = category?.Trim();
            var term = search?.Trim();

            // Fetch ALL first, then filter locally to guarantee behavior
            IEnumerable<Plant> all = _service.GetAllPlants();

            // Category filter (ignore case)
            if (!string.IsNullOrWhiteSpace(cat))
            {
                all = all.Where(p =>
                    !string.IsNullOrEmpty(p.Category) &&
                    p.Category.Equals(cat, StringComparison.InvariantCultureIgnoreCase));
            }

            // Search across Name and Description (ignore case)
            if (!string.IsNullOrWhiteSpace(term))
            {
                all = all.Where(p =>
                    (!string.IsNullOrEmpty(p.Name) &&
                        p.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase)) ||
                    (!string.IsNullOrEmpty(p.Description) &&
                        p.Description.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
            }

            // Total before paging
            var total = all.Count();

            // Clamp page so we don't render an empty page past the end
            var maxPage = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            if (page > maxPage) page = maxPage;

            // Page
            var items = all
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // View model
            var vm = new CatalogViewModel
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Q = term
            };

            // For the category dropdown (or component)
            ViewBag.Category = cat; // current selection (null/empty => All plants)
            ViewBag.Categories = _service.GetAllPlants()
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .OrderBy(c => c)
                .ToList();

            return View(vm);
        }
    }
}
