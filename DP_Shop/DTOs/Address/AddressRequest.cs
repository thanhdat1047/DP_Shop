using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Address
{
    public class AddressRequest
    {

        [Required]
        public string Detail { get; set; } = string.Empty;
        [Required]
        public required string WardCode { get; set; }
    }
}
