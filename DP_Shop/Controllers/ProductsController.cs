using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.Products;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRespository _productRespository; 
        public ProductsController(IProductRespository productRespository)
        {
            _productRespository = productRespository; 
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("admin")]
        public async Task<IActionResult> CreateProduct(ProductWithImagesRequest request)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var result = await _productRespository.CreateAsync(request);

            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetProducts([FromQuery] QueryProducts query)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);  
            }
            var result = await _productRespository.GetAll(query);
            if(result.Succeeded)
            {
                return Ok(result.Data); 
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("list/category/{id}")]
        public async Task<IActionResult> GetProductByCategoryId([FromRoute] int id,[FromQuery] QueryProducts query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productRespository.GetProductByCategoryId(query,id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productRespository.GetById(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetProductBySlug(string slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productRespository.GetBySlug(slug);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _productRespository.ProductExists(id))
            {
                return NotFound("Product not found");
            }

            var result = await _productRespository.DeleteById(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, [FromBody] UpdateProductRequest productRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _productRespository.ProductExists(id))
            {
                return NotFound("Product not found");
            }

            var result = await _productRespository.UpdateAsync(id, productRequest);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/soft-deleted-list")]
        public async Task<IActionResult> GetSoftDeletedList([FromQuery] QueryProducts query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productRespository.GetSoftDeletedList(query);

            if (result == null)
            {
                return BadRequest(new { message = "List of product is null" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("admin/soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _productRespository.ProductExists(id))
            {
                return NotFound("Product not found");
            }

            var result = await _productRespository.SoftDeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/restore/{id}")]
        public async Task<IActionResult> RestoreProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _productRespository.ProductExists(id))
            {
                return NotFound("Category not found");
            }

            var result = await _productRespository.RestoreAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }    
}