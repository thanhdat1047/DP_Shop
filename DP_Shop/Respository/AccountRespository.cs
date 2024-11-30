using DP_Shop.Data.Entities;
using DP_Shop.Interface;
using DP_Shop.Models;
using DP_Shop.Services;
using Microsoft.AspNetCore.Identity;

namespace DP_Shop.Respository
{
    public class AccountRespository : IAccountRespository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AccountRespository(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        public async Task<bool> AddRole(string role)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(role));
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AssignRole(ApplicationUser user, UserRole model)
        {
            
            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<string> Login(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                if (user != null)
                {
                    var userRole = await _userManager.GetRolesAsync(user);
                    return _tokenService.CreateToken(user, userRole);
                }
            }
            return null;
        }

        public async Task<bool> Register(Register model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded) return true;
            return false;

        }
    }
}
