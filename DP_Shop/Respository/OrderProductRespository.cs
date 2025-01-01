using DP_Shop.Data;
using DP_Shop.DTOs.OrderProduct;
using DP_Shop.DTOs.Orders;
using DP_Shop.DTOs.Products;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Microsoft.EntityFrameworkCore;

namespace DP_Shop.Respository
{
    public class OrderProductRespository : IOrderProductRespository
    {
        private readonly AppDbContext _context;
        public OrderProductRespository(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<Result<OrderProductResponse>> GetOrderProductById(int id)
        {
            try
            {
                var orderProduct = await _context.OrderProducts
                    .Include(o =>o.Product)
                    .FirstOrDefaultAsync(o => o.Id == id);
                if (orderProduct == null)
                {
                    return new Result<OrderProductResponse>("Order Product not found");
                }

                var orderResponse = new OrderProductResponse
                {
                    Id = orderProduct.Id,
                    Quantity = orderProduct.Quantity,
                    OrderId = orderProduct.OrderId,
                    Product = orderProduct.Product != null ? orderProduct.Product.ToProductDtoResponse() : new ProductDtoResponse()

                };

                return new Result<OrderProductResponse>(orderResponse);
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<OrderProductResponse>(errorMessage);
            }

        }
    }
}
