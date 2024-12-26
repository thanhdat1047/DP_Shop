using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Users
{
    public class UserResponse
    {
        public string Id { get; set; }  = string.Empty; 
        public string? Username { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public List<string>? Roles { get; set; }
    }
}
