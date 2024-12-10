﻿namespace DP_Shop.DTOs.Address
{
    public class UserAddressResponse
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool isDefault { get; set; } = false;
    }
}
