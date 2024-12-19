using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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

        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
       
        [Required]
        public DateTime CreatedAt { get; set; }  = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        [MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        public void GenerateSlug()
        {
            Slug = Regex.Replace(Name.ToLower().Trim(), @"[^a-z0-9\s-]", "")
                .Replace(" ", "-")
                .Replace("--", "-");
        }
  

    }
}
