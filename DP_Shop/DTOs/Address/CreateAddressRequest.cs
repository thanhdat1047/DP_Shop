using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Address
{
    public class CreateAddressRequest
    {
        [Required]
        public required AddressRequest Address { get; set; }
        public bool IsDefault { get; set; } = false;    
    }
}
