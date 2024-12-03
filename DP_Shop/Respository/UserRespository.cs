using DP_Shop.Data.Entities;
using DP_Shop.Interface;
using Microsoft.AspNetCore.Identity;

namespace DP_Shop.Respository
{
    public class UserRespository : IUserRespository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public UserRespository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApplicationUser> GetUserbyUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}
