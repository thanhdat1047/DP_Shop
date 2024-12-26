using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Carts
{
    public class UpdateCartRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "ProductId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0.")]
        public int ProductId { get; set; }
    }
}
