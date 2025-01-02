using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Images;

namespace DP_Shop.DTOs.Carts
{
    public class CartResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string CategoryName { get; set; } = string.Empty ;
        public CartProduct? Product { get; set; }
        public decimal Total { get; set; } = 0;
        public List<ImageDto>? ListImage { get; set; } 
    }
}
