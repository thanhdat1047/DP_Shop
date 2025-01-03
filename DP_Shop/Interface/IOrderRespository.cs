﻿using DP_Shop.Data.Entities;
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
    }
}
