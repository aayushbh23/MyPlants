using Microsoft.AspNetCore.Mvc;
using PlantsCatalog.Services;
using PlantsCatalog.Models;

namespace PlantsCatalog.Controllers
{
    // Handles all shopping cart and checkout operations
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
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            ViewData["CartTotal"] = cartItems.Sum(item => item.LineTotal);
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _cartService.RemoveItem(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Checkout")]
        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartItems();

            // Prevent checkout when the cart is empty
            if (cart.Count == 0)
            {
                TempData["Message"] = "Your cart is empty. Please add items before checking out.";
                return RedirectToAction("Index");
            }

            // Pre-fill expiry date with a future default value
            return View(new CheckoutForm { ExpiryMonth = 1, ExpiryYear = 2026 });
        }

        [HttpPost]
        [Route("Checkout")]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CheckoutForm form)
        {
            var cart = _cartService.GetCartItems();

            if (cart.Count == 0)
            {
                TempData["Message"] = "Your cart is empty. Please add items before checking out.";
                return RedirectToAction("Index");
            }

            // Return user to form if validation fails
            if (!ModelState.IsValid)
                return View(form);

            // On success — clear cart and show confirmation
            _cartService.ClearCart();
            TempData["OrderSuccess"] = $"Thanks {form.FullName}! Your order has been placed.";
            return RedirectToAction(nameof(Success));
        }

        [HttpGet]
        [Route("OrderSuccess")]
        public IActionResult Success()
        {
            return View();
        }
    }
}
