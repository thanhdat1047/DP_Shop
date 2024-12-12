using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Address
{
    public class AddressModel
    {
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}
