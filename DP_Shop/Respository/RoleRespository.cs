using DP_Shop.DTOs.Enum;
using DP_Shop.Interface;
using Microsoft.AspNetCore.Identity;

namespace DP_Shop.Respository
{
    public class RoleRespository : IRoleRespository
    {
        private readonly RoleManager<IdentityRole> _roleManager; 
        public RoleRespository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> AddRoleAsync(Role role)
        {
            if(role == Role.User || role == Role.Admin)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RoleExistsAsync(Role role)
        {
            return await _roleManager.RoleExistsAsync(role.ToString()); 
        }
    }
}
