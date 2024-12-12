using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Users;
using DP_Shop.Interface;
using DP_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRespository _userRespository;
        private readonly IRoleRespository _roleRespository;
        private readonly IAccountRespository _accountRespository;

        public AccountController(IAccountRespository accountRespository, IUserRespository userRespository, IRoleRespository roleRespository)
        {
            _userRespository = userRespository;
            _roleRespository = roleRespository;
            _accountRespository = accountRespository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            var result =  await _accountRespository.Register(model);
            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfull" });
            }
            return BadRequest(new { message = result.ErrorMessage });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountRespository.Login(model);
            if (result != null)
            {
                var refreshToken = _accountRespository.GenerateRefreshToken();
                await _accountRespository.SaveRefreshTokenAsync(model.Username, refreshToken);

                return Ok( new 
                { 
                    token = result,
                    refreshToken 
                    
                });
            }
            return Unauthorized(new {message = "Username or password is incorrect."});
        }


        /*[Authorize(Roles = "Admin")]
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            if (!await _roleRespository.RoleExistsAsync(role))
            {
                var result = await _roleRespository.AddRoleAsync(role);
                if (result)
                {
                    return Ok(new { messsage = "Role added successfully" });
                }
                return BadRequest(new { messsage = "Couldn't added new role" });
            }
            return BadRequest("Role already exists");
        }
        */

        [Authorize(Roles = "Admin")]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole userRole)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountRespository.AssignRole(userRole);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }

            return BadRequest(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
        {
            var isValid = await _accountRespository.ValidateRefreshTokenAsync(model.Username, model.RefreshToken);
            if(!isValid)
            {
                return Unauthorized(new {message = "Invalid refresh token"});
            }
            var newAccessToken = await _accountRespository.GenerateAccessToken(model);
            if (!newAccessToken.Succeeded)
            {
                return BadRequest(new { message = newAccessToken.ErrorMessage });
            }
            var newRefreshToken = _accountRespository.GenerateRefreshToken();

            await _accountRespository.SaveRefreshTokenAsync(model.Username, newRefreshToken);

            return Ok(new
            {
                accessToken = newAccessToken.Data.AccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest model)
        {
            if (string.IsNullOrEmpty(model.Username))
            {
                return BadRequest(new { message = "Username is required." });
            }

            var result = await _accountRespository.Logout(model.Username);
            if (result.Succeeded)
            {
                return Ok(new { message = "Logout successful." });
            }

            return BadRequest(new { message = result.ErrorMessage });
        }


    }
}
