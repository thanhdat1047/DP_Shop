using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.OrderProduct;
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

                if (requests.Carts == null || !requests.Carts.Any())
                {
                    return new Result<CreateOrderResponse>("Cart IDs are required.");
                }

                var carts = await _context.Carts
                    .Where(c => requests.Carts.Contains(c.Id) && c.UserId == userId)
                    .Include(c => c.Product)
                    .ToListAsync();

                if (carts.IsNullOrEmpty() || carts.Any(c => c.Product == null))
                {
                    return new Result<CreateOrderResponse>("Some carts have invalid or missing products");
                }

                // Check quantity
                var insufficientProducts = carts
                    .Where(c => c.Quantity > c.Product?.Quantity)
                    .Select(c => new
                    {
                        ProductId = c.ProductId,
                        AvailableQuantity = c.Product?.Quantity ?? 0,
                        RequestedQuantity = c.Quantity
                    })
                    .ToList();

                if (insufficientProducts.Count != 0)
                {
                    var insufficientProductDetails = string.Join(", ", insufficientProducts.Select(p =>
                    $"ProductId: {p.ProductId}, Available: {p.AvailableQuantity}, Requested: {p.RequestedQuantity}"));

                    return new Result<CreateOrderResponse>($"Insufficient product quantities: {insufficientProductDetails}");
                }


                decimal totalAmount = carts.Sum(cart => cart.Quantity * (cart.Product?.Price ?? 0));

                // Start transaction
                transaction = await _context.Database.BeginTransactionAsync();

                // Update quantity of product
                foreach(var cart in carts)
                {
                    if(cart.Product != null)
                    {
                        cart.Product.Quantity -= cart.Quantity;
                        _context.Products.Update(cart.Product);
                    }
                }
                // CreateOrder
                var order = new Order
                {
                    Name = $"Order_{DateTime.Now:yyyyMMddHHmmss}",
                    Total = totalAmount,
                    Status = OrderStatus.Pending,
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                    OrderProducts = carts.Select(x=> new OrderProduct
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity
                    }).ToList()
                };

                _context.Orders.Add(order);
                _context.Carts.RemoveRange(carts);

                await _context.SaveChangesAsync();

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
                    .FirstOrDefaultAsync();

                if (order == null) 
                {
                    return new Result<OrderResponse>("Order not found");
                }

                // Change status logic
                if (order.Status == OrderStatus.Pending && status == OrderStatus.Processing)
                {
                    order.Status = status;
                }
                else if (order.Status == OrderStatus.Processing && status == OrderStatus.Completed)
                {
                    order.Status = status;  
                }
                else if(order.Status != OrderStatus.Completed && status == OrderStatus.Cancelled)
                {
                    order.Status = status;

                    var orderProducts = await _context.OrderProducts
                        .Where(o => o.OrderId == orderId)
                        .Include(o => o.Product)
                        .ToListAsync();

                    foreach (var od in orderProducts)
                    {
                        if (od.Product != null)
                        {
                            od.Product.Quantity += od.Quantity;
                            _context.Products.Update(od.Product);
                        }
                    }
                }
                else
                {
                    return new Result<OrderResponse>("Invalid status transition");
                }
                _context.Update(order);
                await _context.SaveChangesAsync();

                return await GetOrderById(userId, orderId);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<OrderResponse>(errorMessage);
            }
        }
        public async Task<Result<OrderResponse>> GetOrderById(string userId, int orderId)
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
                    .AsNoTracking()
                    .Where(o => o.Id == orderId && o.UserId == userId)
                    .Include(o => o.OrderProducts!)
                        .ThenInclude(op => op.Product)
                        .ThenInclude(p => p!.ProductImages)
                        .ThenInclude(pi => pi.Image)
                    .FirstOrDefaultAsync();

                var orderResponse = new OrderResponse()
                {
                    Id = order!.Id,
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
                            } : null,
                            Images = op.Product != null && op.Product.ProductImages != null
                            ? op.Product.ProductImages
                                .Where(pi => pi.Image != null)
                                .Select(pi => new ImageDto
                                {
                                    Id = pi.Image!.Id,
                                    Url = pi.Image.Url
                                }).ToList()
                            : new List<ImageDto>()
                        }).ToList() : new List<OrderProductDto>()
                };

                return new Result<OrderResponse>(orderResponse);

            }
            catch(Exception ex)
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
                        .ThenInclude(p => p!.ProductImages)
                        .ThenInclude(pi => pi.Image)
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
                        } : null,
                        Images = op.Product != null && op.Product.ProductImages != null
                            ? op.Product.ProductImages
                                .Where(pi => pi.Image != null)
                                .Select(pi => new ImageDto
                                {
                                    Id = pi.Image!.Id,
                                    Url = pi.Image.Url
                                }).ToList()
                            : new List<ImageDto>()
                    }).ToList(),

                }).ToList();

                return new Result<List<OrderResponse>>(orderResponses);
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<OrderResponse>>(errorMessage);
            }
        }

        // tong so don hang
        public async Task<Result<OrderAdminResponse>> GetTotalOrders(QueryOrder query)
        {
            /*try
            {
                var total = await _context.Orders.CountAsync();
                return new Result<int>(total);
            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<int>(errorMessage);
            }*/

            try
            {
                var total = await _context.Orders.CountAsync();

                if (!query.IsValidDateRange(out var errorMessage))
                {
                    return new Result<OrderAdminResponse>(errorMessage);
                }

                var ordersQuery = _context.Orders
                    .AsNoTracking();

                if (query.Status != null)
                {
                    ordersQuery = ordersQuery.Where(o => o.Status == query.Status);
                }

                if (query.StartDate.HasValue)
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
                        .ThenInclude(p => p!.ProductImages)
                        .ThenInclude(pi => pi.Image)
                    .ToListAsync();


                var orderResponses = orders.Select(o => new OrderResponse
                {
                    Id = o.Id,
                    Name = o.Name,
                    Total = o.Total,
                    Status = Enum.GetName(typeof(OrderStatus), o.Status)!.ToString(),
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
                        } : null,
                        Images = op.Product != null && op.Product.ProductImages != null
                            ? op.Product.ProductImages
                                .Where(pi => pi.Image != null)
                                .Select(pi => new ImageDto
                                {
                                    Id = pi.Image!.Id,
                                    Url = pi.Image.Url
                                }).ToList()
                            : new List<ImageDto>()
                    }).ToList(),

                }).ToList();

                var orderResponse = new OrderAdminResponse()
                {
                    orderAdminResponses = orderResponses,
                    total = total
                };
                return new Result<OrderAdminResponse>(orderResponse);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<OrderAdminResponse>(errorMessage);
            }
        }

        // tong danh thu cua cac don hang da hoan thanh
        public async Task<Result<decimal>> GetTotalRevenue()
        {
            try
            {
                var total = await _context.Orders
                    .Where(o => o.Status == OrderStatus.Completed)
                    .SumAsync(o => o.Total);

                return new Result<decimal>(total);
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<decimal>(errorMessage);
            }
        }

        // tong so san phan da ban ra
        public async Task<Result<int>> GetProductSalesCount()
        {
            try
            {
                var total = await _context.OrderProducts.SumAsync(op => op.Quantity);
                return new Result<int>(total);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<int>(errorMessage);
            } 
        }

        // Danh thu chi tiet cua tung sp
        public async Task<Result<Dictionary<string, decimal>>> GetRevenueByProduct()
        {
            try
            {
                var result = await _context.OrderProducts.AsNoTracking()
                .Include(op => op.Product)
                .GroupBy(op => op.Product!.Name)
                .Select(group => new
                {
                    ProductName = group.Key,
                    Revenue = group.Sum(op => op.Quantity * op.Product!.Price)
                })
                .ToDictionaryAsync(x => x.ProductName, x => x.Revenue);

                return new Result<Dictionary<string, decimal>>(result);
            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<Dictionary<string, decimal>>(errorMessage);
            }
            
        }

        // So luong don hang theo khoang thoi gian
        public async Task<Result<Dictionary<DateTime, int>>> GetOrdersCountByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _context.Orders.AsNoTracking()
                    .Where(o => o.CreatedAt.Date >= startDate.Date && o.CreatedAt.Date <= endDate.Date)
                    .GroupBy(o => o.CreatedAt.Date)
                    .Select(group => new
                    {
                        Date = group.Key,
                        Count = group.Count(),
                    })
                    .ToDictionaryAsync(x => x.Date, x => x.Count);
                return new Result<Dictionary<DateTime, int>>(result);
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<Dictionary<DateTime, int>>(errorMessage);
            }
        }

        
    }
}
