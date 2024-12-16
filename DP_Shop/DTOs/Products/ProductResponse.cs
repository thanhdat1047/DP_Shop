using DP_Shop.DTOs.Images;

namespace DP_Shop.DTOs.Products
{
    public class ProductResponse
    {
        public ProductDto? Product { get; set; }
        public required List<ImageDto> ImageDtos { get; set; }
    }
}
