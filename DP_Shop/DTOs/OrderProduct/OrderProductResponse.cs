using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.Products;

namespace DP_Shop.DTOs.OrderProduct
{
    public class OrderProductResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public ProductDtoResponse? Product { get; set; }
        public List<ImageDto>? ListImage { get; set; }
    }
}
