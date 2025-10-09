using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlantsCatalog.Models;

namespace PlantsCatalog.Controllers
{
    // Basic site pages and form handling
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) => _logger = logger;

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        [Route("About")]
        public IActionResult About() => View();

        [HttpGet]
        [Route("Contact")]
        public IActionResult Contact() => View();

        [HttpPost]
        [Route("Contact")]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactForm form)
        {
            // Show success message after valid form submission
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Thank you, your message has been submitted successfully!";
                return RedirectToAction("Contact");
            }

            return View(form);
        }

        [Route("Privacy")]
        public IActionResult Privacy() => View();

        // Catch-all for unhandled errors with diagnostic info
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}
