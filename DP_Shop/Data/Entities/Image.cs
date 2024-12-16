using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
