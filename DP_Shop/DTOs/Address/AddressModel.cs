using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Address
{
    public class AddressModel
    {
        [Required]
        public string Detail { get; set; } = string.Empty;
        [Required]
        public required string WardCode { get; set; }

        public string Path_With_Type { get; set; } = string.Empty;
    }
    public class AddressDto
    {
        public int Id { get; set; } 
        [Required]
        public string Detail { get; set; } = string.Empty;
        [Required]
        public required string WardCode { get; set; }

        public string Path_With_Type { get; set; } = string.Empty;
    }
}
