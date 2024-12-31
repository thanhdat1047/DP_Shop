using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Orders
{
    public class CreateOrderRequest
    {
        [Required]
        public required List<int> Carts { get; set; }
    }
}
