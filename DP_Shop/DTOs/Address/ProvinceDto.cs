﻿using DP_Shop.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DP_Shop.DTOs.Address
{
    public class ProvinceDto
    {
        [Key]
        [MaxLength(10)]
        [JsonPropertyName("code")]
        public required string Code { get; set; }

        [Required]
        [MaxLength(255)]
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [JsonPropertyName("name_with_type")]
        public required string Name_With_Type { get; set; }

        [MaxLength(50)]
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [MaxLength(255)]
        [JsonPropertyName("slug")]
        public required string Slug { get; set; }

        public List<District>? Districts { get; set; }
    }
}
