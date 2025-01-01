using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Orders;
using DP_Shop.DTOs.Products;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace DP_Shop.Respository
{
    public class OrderRespository : IOrderRespository
    {
        private readonly AppDbContext _context;
        public OrderRespository(AppDbContext context)
        {
            _context = context;   
        }

        
        public async Task<Result<CreateOrderResponse>> CreateOrder(string userId, CreateOrderRequest requests)
        {
            IDbContextTransaction? transaction = null;
            try
            {
                var existUser = await _context.Users.AnyAsync(u => u.Id == userId);
                if (!existUser)
                {
                    return new Result<CreateOrderResponse>("User not found");
                }

                var carts = await _context.Carts
                    .Where(c => requests.Carts.Contains(c.Id) && c.UserId == userId)
                    .Include(c => c.Product)
                    .ToListAsync();
                if (carts.IsNullOrEmpty() || !carts.Any())
                {
                    return new Result<CreateOrderResponse>("No valid carts found for the given Cart IDs.");
                }

                var checkQuantity = carts.FirstOrDefault
                    (c => c.Quantity > c.Product?.Quantity);
                if (checkQuantity != null)
                {
                    return new Result<CreateOrderResponse>("Error quantity");
                }

                decimal totalAmount = carts.Sum(cart => cart.Quantity * (cart.Product?.Price ?? 0));

                // Start transaction
                transaction = await _context.Database.BeginTransactionAsync();

                // CreateOrder
                var order = new Order
                {
                    Name = $"Order_{DateTime.Now:yyyyMMddHHmmss}",
                    Total = totalAmount,
                    Status = OrderStatus.Pending,
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Create OrderProduct
                foreach (var cart in carts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity,
                    };
                    _context.OrderProducts.Add(orderProduct);
                }

                // Xóa Cart sau khi tạo Order
                _context.Carts.RemoveRange(carts);
                await _context.SaveChangesAsync();

                // Commit
                await transaction.CommitAsync();

                var response = order.ToCreateOrderResponse();
                return new Result<CreateOrderResponse>(response);

            }
            catch (Exception ex) 
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<CreateOrderResponse>(errorMessage);
            }


        }
        public async Task<Result<OrderResponse>> ChangeOrderStatus(string userId, int orderId, OrderStatus status)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new Result<OrderResponse>("UserId isn't valid");
                }
                var existUser = await _context.Users.AnyAsync(u => u.Id == userId);
                if (!existUser)
                {
                    return new Result<OrderResponse>("User not found");
                }

                var order = await _context.Orders
                    .Where(o => o.Id == orderId && o.UserId == userId)
                    .Include(o => o.OrderProducts!)
                    .ThenInclude(op => op.Product)
                    .FirstOrDefaultAsync();

                if (order == null) 
                {
                    return new Result<OrderResponse>("Order not found");
                }

                order.Status = status;
                _context.Update(order);
                await _context.SaveChangesAsync();

                var orderResponse = new OrderResponse() 
                {
                    Id = order.Id,
                    Name = order.Name,
                    Total = order.Total,
                    Status = Enum.GetName(typeof(OrderStatus), order.Status)!.ToString(),
                    UserId = order.UserId,
                    CreatedAt = order.CreatedAt,
                    OrderProduct = order.OrderProducts != null
                        ? order.OrderProducts.Select(op => new OrderProductDto
                        {
                            Id = op.Id,
                            Quantity = op.Quantity,
                            ProductId = op.ProductId,
                            Product = op.Product != null ? new ProductDtoResponse
                            {
                                Id = op.Product.Id,
                                Name = op.Product.Name,
                                Price = op.Product.Price,
                                Slug = op.Product.Slug,
                                ExpiryDate = op.Product.ExpiryDate
                            } : null
                        }).ToList()
                        : new List<OrderProductDto>()
                };

                return new Result<OrderResponse>(orderResponse);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<OrderResponse>(errorMessage);
            }
        }

        public async Task<Result<List<OrderResponse>>> GetOrdersByUserIdAsync(string userId, QueryOrder query)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new Result<List<OrderResponse>>("UserId isn't valid");
                }

                var existUser = await _context.Users.AnyAsync(u => u.Id == userId);
                if (!existUser)
                {
                    return new Result<List<OrderResponse>>("User not found");
                }

                if (!query.IsValidDateRange(out var errorMessage))
                {
                    return new Result<List<OrderResponse>>(errorMessage);
                }

                var ordersQuery = _context.Orders
                    .Where(o => o.UserId == userId);

                if(query.Status != null)
                {
                    ordersQuery = ordersQuery.Where(o => o.Status == query.Status);
                }
                 
                if(query.StartDate.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.CreatedAt >= query.StartDate.Value);
                }

                if (query.EndDate.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.CreatedAt <= query.EndDate.Value);
                }
                
                if (query.isPriceDecsending)
                {
                    ordersQuery = ordersQuery.OrderByDescending(o => o.Total);
                }
                else
                {
                    ordersQuery = ordersQuery.OrderBy(o => o.Total);
                }

                var orders = await ordersQuery
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .Include(o => o.OrderProducts!)
                        .ThenInclude(op => op.Product)
                    .ToListAsync();


                var orderResponses = orders.Select(o => new OrderResponse
                {
                    Id = o.Id,
                    Name = o.Name,
                    Total = o.Total,
                    Status = Enum.GetName(typeof(OrderStatus),o.Status)!.ToString(),
                    UserId = o.UserId,
                    CreatedAt = o.CreatedAt,
                    OrderProduct = o.OrderProducts!.Select(op => new OrderProductDto
                    {
                        Id = op.Id,
                        Quantity = op.Quantity,
                        ProductId = op.ProductId,
                        Product = op.Product != null ? new ProductDtoResponse
                        {
                            Id = op.Product.Id,
                            Name = op.Product.Name,
                            Price = op.Product.Price,
                            Slug = op.Product.Slug,
                            ExpiryDate = op.Product.ExpiryDate
                        } : null
                    }).ToList()
                }).ToList();

                return new Result<List<OrderResponse>>(orderResponses);
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<OrderResponse>>(errorMessage);
            }
        }
    }
}
