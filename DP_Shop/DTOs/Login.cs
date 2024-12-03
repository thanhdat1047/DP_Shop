using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Username is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage ="Password is required.")]
        [StringLength(100, MinimumLength =6, ErrorMessage ="Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
