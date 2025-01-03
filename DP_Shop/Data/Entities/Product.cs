using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DP_Shop.Data.Entities
{
    public class Product
    {
        private static readonly string DescriptionFolder = Path.Combine(Directory.GetCurrentDirectory(), "Files");

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
        public DateTime CreatedAt { get; set; } = DateTime.Now;

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

        public void SaveDescriptionToFile()
        {
            Directory.CreateDirectory(DescriptionFolder);

            var sanitizedFileName = Regex.Replace(Name.ToLower().Trim(), @"[^a-z0-9\s-]", "")
                .Replace(" ", "_")
                .Replace("--", "_");

            var uniqueIdentifier = Guid.NewGuid();
            string fileName = $"{sanitizedFileName}_{uniqueIdentifier}.txt";

            string filePath = Path.Combine(DescriptionFolder, fileName);

            File.WriteAllText(filePath, Description);

            Description = fileName;
        }

        public string GetDescriptionFromFile()
        {
            string filePath = Path.Combine(DescriptionFolder, Description);

            if (!File.Exists(filePath))
                return string.Empty;

            return File.ReadAllText(filePath);
        }

        public bool UpdateDescriptionFile(string description, string path)
        {
            var descriptionFilePath = Path.Combine(DescriptionFolder, path);

            if (File.Exists(descriptionFilePath))
            {
                File.WriteAllText(descriptionFilePath, description);
                return true;
            }
            return false;
        }

    }
}
