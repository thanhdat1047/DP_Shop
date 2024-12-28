using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Users
{
    public class ChangePassword
    {
        [Required]
        public required string OldPassword { get; set; }
        [Required]
        public required string NewPassword { get; set; } 
    }
}
