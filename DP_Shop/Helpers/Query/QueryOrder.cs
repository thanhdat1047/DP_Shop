using DP_Shop.DTOs.Enum;

namespace DP_Shop.Helpers.Query
{
    public class QueryOrder
    {
        public bool isPriceDecsending { get; set; } = false;
        public OrderStatus? Status { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public DateTime? StartDate { get; set; } // Thời gian bắt đầu
        public DateTime? EndDate { get; set; }

        public bool IsValidDateRange(out string errorMessage)
        {
            if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
            {
                errorMessage = "StartDate cannot be greater than EndDate.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }
    }
}
