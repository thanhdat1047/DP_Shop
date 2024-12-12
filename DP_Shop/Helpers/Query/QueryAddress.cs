namespace DP_Shop.Helpers.Query
{
    public class QueryAddress
    {
        public string City { get; set; } = string.Empty;
        public string? SortBy { get; set; } = null;
        public bool isDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
