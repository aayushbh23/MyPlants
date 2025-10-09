namespace PlantsCatalog.Models
{
    // Represents a single item in the user's shopping cart
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Automatically calculates total cost for this item
        public decimal LineTotal => Price * Quantity;
    }
}
