using DP_Shop.DTOs.Enum;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Orders
{
    public class ChangeStateRequest
    {
        [Required]
        public required string UserId { get; set; }
        public OrderStatus status { get; set; } 
    }
}
