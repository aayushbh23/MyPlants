using Microsoft.AspNetCore.Mvc;
using PlantsCatalog.Services;
using PlantsCatalog.Models;
using System.Linq;

namespace PlantsCatalog.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public IActionResult Add(int id, int quantity)
        {
            _cartService.AddItem(id, quantity);
            // After adding, redirect to cart overview
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            // Calculate total
            decimal total = cartItems.Sum(item => item.LineTotal);
            // You could use a ViewModel to pass both items and total, or use ViewData
            ViewData["CartTotal"] = total;
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _cartService.RemoveItem(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            // Here you could implement order processing; for now, just clear the cart
            _cartService.ClearCart();
            TempData["Message"] = "Checkout complete! Thank you for your purchase.";
            return RedirectToAction("Index");
        }
    }
}
