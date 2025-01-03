using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.OrderProduct;
using DP_Shop.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Orders
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<OrderProductDto> OrderProduct { get; set; } = new List<OrderProductDto>();
        
    }
}
