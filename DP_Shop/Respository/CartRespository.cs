using DP_Shop.Data;
using DP_Shop.DTOs.Carts;
using DP_Shop.DTOs.Categories;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using DP_Shop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using DP_Shop.Helpers.Query;
using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Images;

namespace DP_Shop.Respository
{
    public class CartRespository : ICartRespository
    {
        private readonly AppDbContext _dbContext;
        public CartRespository(AppDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task<Result<CartDto>> AddItemToCart(string userId, CreateCartRequest cartRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || userId != cartRequest.UserId)
                {
                    return new Result<CartDto>("UserId does not match or is null");
                }

                var userExists = await _dbContext.Users.AnyAsync(u => u.Id == userId);
                if (!userExists)
                {
                    return new Result<CartDto>("User not found");
                }

                var product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == cartRequest.ProductId);
                if(product == null)
                {
                    return new Result<CartDto>("Product not found");
                }

                if(product.Quantity < cartRequest.Quantity)
                {
                    return new Result<CartDto>("Doesn't enough product");
                }

                var cartModel = cartRequest.ToCart();
                await _dbContext.Carts.AddAsync(cartModel);

                await _dbContext.SaveChangesAsync();

                var cartDto = cartModel.ToCartDto();
                //cartDto.Total = cartDto.Quantity * product.Price;
                
                return new Result<CartDto>(cartDto);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<CartDto>(errorMessage);
            }

        }

        public async Task<Result<List<CartResponse>>> GetListProductInCart(string userId, QueryCart query)
        {
            try
            {
                /*var cartItems = await _dbContext.Carts
                    .Where(c => c.UserId == userId)
                    .Include(c => c.Product)
                    .OrderBy(c => c.Id)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .Select(c => c.ToCartDto())
                    .ToListAsync();*/


                var cartItems = await _dbContext.Carts
                    .Where(c => c.UserId == userId)
                    .Include(c => c.Product)
                        .ThenInclude(p => p.ProductImages)
                    .Include(c => c.Product)
                        .ThenInclude(p => p.Category)
                    .OrderBy(c => c.Id)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .Select(c => new CartResponse
                    {
                        Id = c.Id,
                        Quantity = c.Quantity,
                        UserId = c.UserId,
                        ProductId = c.ProductId,
                        Product = c.Product != null ? new CartProduct
                        {
                            Id = c.Product.Id,
                            Name = c.Product.Name,
                            Price = c.Product.Price,
                            Description = c.Product.GetDescriptionFromFile()
                        } : null,
                        Total = c.TotalPrice,
                        CategoryName = c.Product != null && c.Product.Category != null
                            ? c.Product.Category.Name
                            : string.Empty,
                        ListImage = c.Product != null && c.Product.ProductImages != null
                        ? c.Product.ProductImages
                            .Where(pi => pi.Image != null)
                            .Select(pi => new ImageDto
                            {
                                Id = pi.Image!.Id,
                                Url = pi.Image.Url
                            }).ToList()
                        : new List<ImageDto>()
                    }).ToListAsync();


                return new Result<List<CartResponse>>(cartItems);

            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new  Result<List<CartResponse>> (errorMessage);
            }
        }

        public async Task<Result<bool>> RemoveItemFromCart(string userId, int cartId)
        {

            try
            {
                var userExists = await _dbContext.Users.AnyAsync(u => u.Id == userId);
                if (!userExists)
                {
                    return new Result<bool>("User not found");
                }

                var cart = await _dbContext.Carts
                    .Include( c => c.Product)
                    .SingleOrDefaultAsync(c => c.Id == cartId && c.UserId == userId);
                if (cart == null)
                {
                    return new Result<bool>("Cart not found");
                }
                /*if(cart.UserId != userId)
                {
                    return new Result<bool>("Item is not belong to user");
                }*/

                _dbContext.Carts.Remove(cart);
                await _dbContext.SaveChangesAsync();


                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }

        }

        public async Task<Result<CartDto>> UpdateItemInCart(string userId, UpdateCartRequest cartRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || userId != cartRequest.UserId)
                {
                    return new Result<CartDto>("UserId does not match or is null");
                }

                var userExists = await _dbContext.Users.AnyAsync(u => u.Id == userId);
                if (!userExists)
                {
                    return new Result<CartDto>("User not found");
                }

                var product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == cartRequest.ProductId);
                if (product == null)
                {
                    return new Result<CartDto>("Product not found");
                }

                if (product.Quantity < cartRequest.Quantity)
                {
                    return new Result<CartDto>("Doesn't enough product");
                }

                var cart =  await _dbContext.Carts
                    .Include(c => c.Product)
                    .SingleOrDefaultAsync(c => c.Id == cartRequest.Id && c.UserId == userId);

                if (cart == null)
                {
                    return new Result<CartDto>("Cart not found");
                }

                cart.Quantity = cartRequest.Quantity;   

                _dbContext.Carts.Update(cart);
                await _dbContext.SaveChangesAsync();

                var cartDto = cart.ToCartDto();
                //cartDto.Total = cart.TotalPrice;

                return new Result<CartDto>(cartDto);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<CartDto>(errorMessage);
            }

        }
    }
}
