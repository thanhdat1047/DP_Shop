using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Images;
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
                var orderProduct = _context.OrderProducts
                    .Include(op => op.Product)
                    .ThenInclude(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                    .FirstOrDefault(op => op.Id == id);


                if (orderProduct == null)
                {
                    return new Result<OrderProductResponse>("Order Product not found");
                }

                var orderResponse = new OrderProductResponse
                {
                    Id = orderProduct.Id,
                    Quantity = orderProduct.Quantity,
                    OrderId = orderProduct.OrderId,
                    Product = orderProduct.Product != null ? orderProduct.Product.ToProductDtoResponse() : new ProductDtoResponse(),
                    ListImage = orderProduct.Product != null && orderProduct.Product.ProductImages != null
                    ? orderProduct.Product.ProductImages
                        .Where(pi => pi.Image != null)
                        .Select(pi => new ImageDto
                        {
                            Id = pi.Image!.Id,
                            Url = pi.Image.Url
                        }).ToList()
                    : new List<ImageDto>()
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
