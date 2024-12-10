using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Users
{
    public class LogoutRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
    }
}
