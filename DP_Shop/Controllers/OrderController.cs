using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Orders;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Models.Result;
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
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] int orderId, [FromBody] ChangeStateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var userID = User.FindFirst("userId");

            var userID = request.UserId;
            if (userID == null)
            {
                return NotFound("UserId isn't valid");
            }


            var result = await _orderRespository.ChangeOrderStatus(userID, orderId, request.status);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/total-orders")]
        public async Task<IActionResult> GetTotalOrders([FromQuery] QueryOrder query)
        {
            var result = await _orderRespository.GetTotalOrders(query);
            if(result.Succeeded)
            {
                return Ok(new
                {
                    orders =  result.Data.orderAdminResponses,
                    total = result.Data.total
                }); 
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/total-revenue")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            var result = await _orderRespository.GetTotalRevenue();
            if (result.Succeeded)
            {
                return Ok(new { TotalRevenue = result.Data });
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/product-sales")]
        public async Task<IActionResult> GetProductSalesCount()
        {
            var result = await _orderRespository.GetProductSalesCount();
            if (result.Succeeded)
            {
                return Ok(new { ProductSales = result.Data });
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/revenue-by-product")]
        public async Task<IActionResult> GetRevenueByProduct()
        {
            var result = await _orderRespository.GetRevenueByProduct();
            if (result.Succeeded)
            {
                return Ok(result.Data );
            }
            return BadRequest(result.ErrorMessage);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("admin/orders-by-date")]
        public async Task<IActionResult> GetOrdersCountByDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _orderRespository.GetOrdersCountByDate(startDate, endDate);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
