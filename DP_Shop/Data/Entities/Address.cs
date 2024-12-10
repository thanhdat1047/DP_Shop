using System.ComponentModel.DataAnnotations;

namespace DP_Shop.Data.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Code { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; }
        
    }
}
