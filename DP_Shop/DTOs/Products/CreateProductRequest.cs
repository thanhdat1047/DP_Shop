namespace DP_Shop.DTOs.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int CategoryId { get; set; }
    }
}
