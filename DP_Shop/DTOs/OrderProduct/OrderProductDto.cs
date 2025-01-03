using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.OrderProduct
{
    public class OrderProductDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public ProductDtoResponse? Product { get; set; }
        public List<ImageDto>? Images { get; set; }
    }
}
