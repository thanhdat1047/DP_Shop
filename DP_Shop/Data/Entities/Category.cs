using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty; 
        [Required]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
