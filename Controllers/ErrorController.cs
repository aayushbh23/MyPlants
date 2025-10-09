using Microsoft.AspNetCore.Mvc;

namespace PlantsCatalog.Controllers
{
    // Centralized error handling for clean routing and custom views
    public class ErrorController : Controller
    {
        // Handles general HTTP status errors
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            if (statusCode == 404)
                return View("404NotFound"); // Custom not-found page

            return View("Error"); // Generic fallback error view
        }

        // Dedicated route for unhandled server exceptions (500)
        [Route("Error/500")]
        public IActionResult ServerError() => View("ServerError");
    }
}
