using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int Quantity { get; set; } 
        [Required]
        public string Image { get; set; } = string.Empty;
        public int CategoryId { get; set; }


        public Category Category { get; set; }
  

    }
}
