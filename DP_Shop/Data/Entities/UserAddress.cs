﻿using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class UserAddress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsDefault { get; set; }
        public int AddressId { get; set; }
        public string UserId { get; set; }

        public Address Address { get; set; }
        public ApplicationUser User { get; set; }
    }
}