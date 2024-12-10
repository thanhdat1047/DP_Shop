using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public Product Product { get; set; }    
        public Order Order { get; set; }    
    }
}
