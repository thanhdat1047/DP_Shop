﻿using DP_Shop.DTOs.Users;
using DP_Shop.Helpers.Query;
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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var users = await _userRespository.GetUsers(query);
            if(users != null)
            {
                return Ok(users);
            }
            return NotFound();  
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userRespository.DeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data.ToUserDto());
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
        [HttpGet("profile/")]
        public async Task<IActionResult> GetUserProfile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return NotFound("User not found");
            }
            var result = await _userRespository.GetUserProfile(userID.Value);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);

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
