using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ImageId { get; set; }
        public Product? Product { get; set; }  
        public Image? Image { get; set; }
    }   
}


