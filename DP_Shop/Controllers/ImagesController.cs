using Microsoft.AspNetCore.Mvc;
using DP_Shop.DTOs.Products;
using DP_Shop.DTOs.Users;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Respository;
using Microsoft.AspNetCore.Authorization;
using DP_Shop.DTOs.Images;
namespace DP_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRespository _imageRespository;
        public ImagesController(IImageRespository imageRespository)
        {
            _imageRespository = imageRespository;   
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("list/admin")]
        public async Task<IActionResult> GetImages([FromQuery] QueryImages query)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            var result  = await _imageRespository.GetImages(query);
            if (result.Succeeded) 
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }


        [Authorize(Roles ="Admin")]
        [HttpPost("admin/{productId}")]
        public async Task<IActionResult> AddImage([FromBody] ImageRequest imageRequest,[FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _imageRespository.AddImage(imageRequest,productId);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("admin/{productId}")]
        public async Task<IActionResult> DeleteImageOfProduct([FromBody] ImageDto imageRequest, [FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _imageRespository.DeleteImageOfProduct(imageRequest, productId);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _imageRespository.GetImageById(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetImagesByProductId([FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _imageRespository.GetImagesByProductId(productId);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

    }
}
