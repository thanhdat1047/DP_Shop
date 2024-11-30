using DP_Shop.Data.Entities;
using DP_Shop.Interface;
using DP_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountRespository _accountRespository;

        public AccountController(IAccountRespository accountRespository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountRespository = accountRespository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
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
            var result = await _accountRespository.Login(model);
            if (result!= null) 
            {
                return Ok( result);
            }
            return Unauthorized();
        }

        
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody]string role)
        {
            if(!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _accountRespository.AddRole(role);
                if (result) 
                {
                    return Ok(new { messsage = "Role added successfully" });
                }
                return BadRequest(new { messsage = "Couldn't added new role" });
            }
            return BadRequest("Role already exists");
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await _accountRespository.AssignRole(user,model);
            if (result)
            {
                return Ok(new { message = "Role assigned successfully" });
            }
            return BadRequest(new { messsage = "Couldn't assigned this role" });
        }
    }
}
