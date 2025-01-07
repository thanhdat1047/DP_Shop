using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.OrderProduct;
using DP_Shop.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Orders
{
    public class OrderAdminResponse
    {
        public List<OrderResponse> orderAdminResponses = new List<OrderResponse>();
        public int total { get; set; } = 0;
        
    }
}
