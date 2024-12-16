using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string UserId { get; set; } = string.Empty;  
        public int ProductId { get; set; }


        public ApplicationUser? User { get; set; }
        public Product? Product { get; set; }
    }
}
