using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Enum;
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
            if (result)
            {
                return Ok(new { message = "User registered successfull" });
            }
            return BadRequest(new { message = "Something went wrong" });
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
                return Ok( new { token = result});
            }
            return Unauthorized(new {message = "Username or password is incorrect."});
        }


        //[Authorize(Roles = "Admin")]
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


        [Authorize(Roles = "Admin")]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole userRole)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userRespository.GetUserbyUsername(userRole.Username);
            // Check user not found
            if (user == null)
            {
                return BadRequest("User not found");
            }
            // Check user has this role
            if(await _accountRespository.IsExistsRole(user, userRole))
            {
                return BadRequest(new { message = "User already has this role." });
            }
            // Check failed to remove role
            if (!await _accountRespository.RemoveRole(user, userRole))
            {
                return BadRequest(new { message = "Failed to remove old role." });
            }
            // Check role assigned 
            if (await _accountRespository.AssignRole(user, userRole.Role))
            {
                return Ok(new { message = "Role assigned successfully" });
            }

            return BadRequest(new { messsage = "Couldn't assigned this role" });
        }
    }
}
