using DP_Shop.DTOs.Address;

namespace DP_Shop.DTOs.Users
{
    public class UserProfile
    {
        public string Id { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;

        public List<AddressModel>? Addresses { get; set; }

    }
}
