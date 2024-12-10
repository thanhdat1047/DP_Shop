using DP_Shop.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DP_Shop.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user,IList<string> userRole);
    }
}
