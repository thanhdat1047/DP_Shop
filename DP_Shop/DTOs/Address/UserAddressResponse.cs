namespace DP_Shop.DTOs.Address
{
    public class UserAddressResponse
    {
        public int Id { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string WardCode { get; set; } = string.Empty;
        public bool isDefault { get; set; } = false;
        public string Path_With_Type { get; set; } = string.Empty;
    }
}
