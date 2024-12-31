using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Orders;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRespository _orderRespository;
        public OrderController(IOrderRespository orderRespository)
        {
            _orderRespository = orderRespository;   
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (request.Carts == null || !request.Carts.Any())
            {
                return BadRequest("CartIds cannot be empty.");
            }

            var userID = User.FindFirst("userId");
            if (userID?.Value == null)
            {
                return NotFound("UserId isn't valid");
            }

            var result = await _orderRespository.CreateOrder(userID.Value, request);
            if(result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] QueryOrder query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userID = User.FindFirst("userId");
            if (userID?.Value == null)
            {
                return NotFound("UserId isn't valid");
            }

            var result = await _orderRespository.GetOrdersByUserIdAsync(userID.Value, query);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpPatch("{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] int orderId,[FromQuery] OrderStatus status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = User.FindFirst("userId");
            if (userID?.Value == null)
            {
                return NotFound("UserId isn't valid");
            }

            var result = await _orderRespository.ChangeOrderStatus(userID.Value, orderId, status);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
