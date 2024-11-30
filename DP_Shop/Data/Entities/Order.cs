using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;    
        [Required]
        public decimal Total { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }   

        public ICollection<OrderProduct> OrderProducts { get; set; } 


    }
}
