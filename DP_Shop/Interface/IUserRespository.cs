using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Result;
using DP_Shop.DTOs.Users;
using DP_Shop.Helpers;

namespace DP_Shop.Interface
{
    public interface IUserRespository
    {
        Task<ApplicationUser> GetUserbyUsername(string username);
        Task<List<ApplicationUser>> GetUsers(QueryUser query);
        Task<ApplicationUser> GetUserById(string id);
        Task<Result<ApplicationUser>> UpdateAsync(string id, UpdateUserRequestDto userDto);
        Task<Result<ApplicationUser>> DeleteAsync(string id);
        Task<bool> UserExists(string id);

    }
}
