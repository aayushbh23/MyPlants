using PlantsCatalog.Models;
using System.Collections.Generic;

namespace PlantsCatalog.Services
{
    public interface ICartService
    {
        void AddItem(int productId, int quantity);
        void RemoveItem(int productId);
        List<CartItem> GetCartItems();
        void ClearCart();
    }
}
