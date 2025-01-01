using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Enum;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Orders
{
    public class CreateOrderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
