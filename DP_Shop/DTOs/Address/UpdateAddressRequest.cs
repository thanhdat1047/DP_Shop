using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Address
{
    public class UpdateAddressRequest
    {
        [Required]
        public required AddressRequest Address { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
