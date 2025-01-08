using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Address
{
    public class DeleteAddressRequest
    {
        [Required]
        public required string UserId { get; set; }
    }
}
