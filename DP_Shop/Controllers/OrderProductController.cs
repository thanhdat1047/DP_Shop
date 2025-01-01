using DP_Shop.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductRespository _orderProductRespository;

        public OrderProductController(IOrderProductRespository orderProductRespository)
        {
            _orderProductRespository = orderProductRespository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            var result = await _orderProductRespository.GetOrderProductById(id);
            if(result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);  
        }

    }
}
