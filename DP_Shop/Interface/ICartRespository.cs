using DP_Shop.DTOs.Carts;
using DP_Shop.DTOs.Products;
using DP_Shop.Helpers.Query;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface ICartRespository
    {
        Task<Result<CartDto>> AddItemToCart(string userId, CreateCartRequest cartRequest);   
        Task<Result<Boolean>> RemoveItemFromCart(string userId, int cartId);
        Task<Result<CartDto>> UpdateItemInCart(string userId, UpdateCartRequest cartRequest);
        Task <Result<List<CartDto>>> GetListProductInCart(string userId, QueryCart query);
    }
}
