using DP_Shop.DTOs.Images;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Categories
{
    public class CreateCategoryRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Description { get; set; } = string.Empty;
        [Required]
        public required ImageDto ImageDto { get; set; }

    }
}
