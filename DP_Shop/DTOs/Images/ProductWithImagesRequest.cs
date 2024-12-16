using DP_Shop.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Images
{
    public class ProductWithImagesRequest
    {
        [Required]
        public required CreateProductRequest Product { get; set; }
        [Required]
        public required List<ImageDto> ImageDtos { get; set; } 
    }
}
