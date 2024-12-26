using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Carts;
using DP_Shop.DTOs.Users;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Respository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRespository _cartRespository;
        public CartController(ICartRespository cartRespository)
        {
            _cartRespository = cartRespository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromBody] CreateCartRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return BadRequest("User id is null");
            }

            var result = await _cartRespository.AddItemToCart(userID.Value, request);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateItemInCart([FromBody] UpdateCartRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return BadRequest("User id is null");
            }

            var result = await _cartRespository.UpdateItemInCart(userID.Value, request);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetListItems([FromBody] QueryCart query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return BadRequest("User id is null");
            }

            var result = await _cartRespository.GetListProductInCart(userID.Value, query);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);

        }

        [Authorize]
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> RemoveProductInCart([FromRoute] int cartId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return BadRequest("User id is null");
            }

            var result = await _cartRespository.RemoveItemFromCart(userID.Value, cartId);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);

        }


    }
}
