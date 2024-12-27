using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class UserAddress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsDefault { get; set; } = false;
        public int AddressId { get; set; }
        public string UserId { get; set; } = string.Empty;

        public Address Address { get; set; } = new Address();
        public ApplicationUser? User { get; set; }
    }
}
