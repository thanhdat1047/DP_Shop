using DP_Shop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Carts
{
    public class CartDto
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public decimal Total { get; set; } = 0;
    }
}
