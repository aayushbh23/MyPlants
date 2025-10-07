using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(string? q, int page = 1, int pageSize = 6)
        {
            var all = _service.GetAll();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var ql = q.Trim().ToLowerInvariant();
                all = all.Where(p =>
                    p.Name.ToLowerInvariant().Contains(ql) ||
                    p.Description.ToLowerInvariant().Contains(ql));
            }

            var total = all.Count();
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
                Q = q
            };

            return View(vm);
        }
    }
}
