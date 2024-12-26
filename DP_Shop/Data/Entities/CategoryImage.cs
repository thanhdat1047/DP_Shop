using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class CategoryImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ImageId { get; set; }
        public Category? Category { get; set; }  
        public Image? Image { get; set; }
    }   
}


