using DP_Shop.DTOs.Images;

namespace DP_Shop.DTOs.Categories
{
    public class CategoryResponse
    {
        public CategoryDto? CategoryDto { get; set; }   
        public required ImageDto? ImageDto { get; set; }   

    }
}
