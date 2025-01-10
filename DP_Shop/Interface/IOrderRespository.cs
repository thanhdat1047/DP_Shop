using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Orders;
using DP_Shop.Helpers.Query;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface IOrderRespository
    {
        Task<Result<CreateOrderResponse>> CreateOrder(string userId, CreateOrderRequest requests);
        Task<Result<List<OrderResponse>>> GetOrdersByUserIdAsync(string userId, QueryOrder query);
        Task<Result<OrderResponse>> ChangeOrderStatus(string userId, int orderId,OrderStatus status);
        Task<Result<OrderResponse>> GetOrderById(string userId, int orderId);
        Task<Result<OrderAdminResponse>> GetTotalOrders(QueryOrder query);
        Task<Result<decimal>> GetTotalRevenue();
        Task<Result<int>> GetProductSalesCount();
        Task<Result<Dictionary<string, decimal>>> GetRevenueByProduct();
        Task<Result<Dictionary<DateTime, int>>> GetOrdersCountByDate(DateTime startDate, DateTime endDate);

    }
}
