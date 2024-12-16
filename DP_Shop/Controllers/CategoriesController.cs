using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Categories;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Respository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController: ControllerBase
    {
        private readonly ICategoryRespository _categoryRespository;
        public CategoriesController(ICategoryRespository categoryRespository)
        {
            _categoryRespository = categoryRespository; 
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryRespository.CreateAsync(request);   
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

            if (!await _categoryRespository.CategoryExists(id))
            {
                return NotFound("Category not found");
            }

            var result = await _categoryRespository.DeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _categoryRespository.CategoryExists(id))
            {
                return NotFound("Category not found");
            }

            var result = await _categoryRespository.UpdateAsync(id, categoryDto);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategorysById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _categoryRespository.CategoryExists(id))
            {
                return NotFound("Category not found");
            }

            var result = await _categoryRespository.GetById(id);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryRespository.GetAll();

            if (result == null)
            {
                return BadRequest(new { message = "List of category is null" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/soft-deleted-list")]
        public async Task<IActionResult> GetSoftDeletedList()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryRespository.GetSoftDeletedList();

            if (result == null)
            {
                return BadRequest(new { message = "List of category is null" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _categoryRespository.CategoryExists(id))
            {
                return NotFound("Category not found");
            }

            var result = await _categoryRespository.SoftDeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/restore/{id}")]
        public async Task<IActionResult> RestoreCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _categoryRespository.CategoryExists(id))
            {
                return NotFound("Category not found");
            }

            var result = await _categoryRespository.RestoreAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
