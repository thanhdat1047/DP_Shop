using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Images
{
    public class ImageRequest
    {
        [Required(ErrorMessage = "Url is required.")]
        [Url(ErrorMessage = "The Url field is not a valid URL.")]
        public string Url { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Description cannot exceed 50 characters.")]
        public string? Description { get; set; } = string.Empty;
    }

}

