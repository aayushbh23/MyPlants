using PlantsCatalog.Models;

namespace PlantsCatalog.Services
{
    // Defines contract for managing shopping cart operations
    public interface ICartService
    {
        void AddItem(int productId, int quantity);  // Add or increase quantity
        void RemoveItem(int productId);             // Remove specific item
        List<CartItem> GetCartItems();              // Retrieve all items in cart
        void ClearCart();                           // Empty the cart
    }
}
