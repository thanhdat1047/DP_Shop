using DP_Shop.DTOs.Users;
using DP_Shop.Helpers;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static IUserRespository _userRespository;

        public UserController(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromBody] QueryUser query)
        {
            var users = await _userRespository.GetUsers(query);
            var listUserDto = users.Select(s => s.ToUserDto());
            return Ok(listUserDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userRespository.DeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByUserID([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userRespository.GetUserById(id);
            if (user == null)
            {
                return NotFound();  
            }
            return Ok(user.ToUserDto()); 

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserByID([FromRoute] string id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userRespository.UpdateAsync(id, updateUserRequestDto);
            if (result.Succeeded)
            {
                return Ok(result.Data.ToUserDto());
            }
            return BadRequest(result.ErrorMessage);

        }
    }
}
