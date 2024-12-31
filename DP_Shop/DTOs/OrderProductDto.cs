using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs
{
    public class OrderProductDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public ProductDtoResponse? Product { get; set; }
    }
}
