using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Carts;
using DP_Shop.DTOs.Orders;

namespace DP_Shop.Mappers
{
    public static class OrderMappers
    {
        public static CreateOrderResponse ToCreateOrderResponse(this Order order)
        {
            return new CreateOrderResponse
            {
                Id = order.Id,
                Name = order.Name,
                UserId = order.UserId,
                Total = order.Total,
                Status = order.Status,
                CreatedAt = DateTime.Now,
            };
        }

       /* public static OrderResponse ToOrderResponse(this Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                Name = order.Name,
                UserId = order.UserId,
                Total = order.Total,
                Status = order.Status,
                CreatedAt = DateTime.Now,
                OrderProducts = order.OrderProducts
            };
        }*/
    }
}
