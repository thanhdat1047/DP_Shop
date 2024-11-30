using Microsoft.AspNetCore.Identity;

namespace DP_Shop.Data.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public ICollection<Cart> Carts { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
