﻿using DP_Shop.DTOs.Enum;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Total { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ApplicationUser? User { get; set; }

        public ICollection<OrderProduct>? OrderProducts { get; set; }


    }
}
