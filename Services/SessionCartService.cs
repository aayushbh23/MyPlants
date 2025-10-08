using PlantsCatalog.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace PlantsCatalog.Services
{
    public class SessionCartService : ICartService
    {
        private const string SessionKey = "CartItems";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPlantService _plantService;

        public SessionCartService(IHttpContextAccessor httpContextAccessor, IPlantService plantService)
        {
            _httpContextAccessor = httpContextAccessor;
            _plantService = plantService;
        }

        public void AddItem(int productId, int quantity)
        {
            var cart = GetCartItems();
            var existing = cart.FirstOrDefault(ci => ci.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                var product = _plantService.GetById(productId);
                if (product == null) return;

                cart.Add(new CartItem {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }
            SaveCart(cart);
        }

        public void RemoveItem(int productId)
        {
            var cart = GetCartItems();
            cart.RemoveAll(ci => ci.ProductId == productId);
            SaveCart(cart);
        }

        public List<CartItem> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return new List<CartItem>();

            string? cartJson = session.GetString(SessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }
        public void ClearCart()
        {
            SaveCart(new List<CartItem>());
        }
        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return;
            session.SetString(SessionKey, JsonSerializer.Serialize(cart));
        }
    }
}
