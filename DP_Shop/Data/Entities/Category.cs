﻿using System.ComponentModel.DataAnnotations;

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
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
