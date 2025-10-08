using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlantsCatalog.Models;

namespace PlantsCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("Contact")]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactForm form)
        {
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Thank you, your message has been submitted successfully!";
                return RedirectToAction("Contact");
            }
            return View(form);
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
