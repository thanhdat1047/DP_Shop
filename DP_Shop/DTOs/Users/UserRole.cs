using DP_Shop.DTOs.Enum;
using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Users
{
    public class UserRole
    {

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        [EnumDataType(typeof(Role), ErrorMessage = "Invalid role.")]
        public Role Role { get; set; }
    }
}
