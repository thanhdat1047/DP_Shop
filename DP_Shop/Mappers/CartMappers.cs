using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Carts;

namespace DP_Shop.Mappers
{
    public static class CartMappers
    {
        public static Cart ToCart(this CreateCartRequest createCart)
        {
            return new Cart
            {
                Quantity = createCart.Quantity,
                ProductId = createCart.ProductId,
                UserId = createCart.UserId,
            };
        }
        public static Cart ToCart(this CartDto cartDto)
        {
            return new Cart
            {
                Id = cartDto.Id,    
                Quantity = cartDto.Quantity,
                ProductId = cartDto.ProductId,
                UserId = cartDto.UserId,
            };
        }

        public static CartDto ToCartDto (this Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                Quantity = cart.Quantity,
                ProductId = cart.ProductId,
                UserId = cart.UserId,
                Total = cart.TotalPrice,
                Product = cart.Product
            };
        }

    }
}
