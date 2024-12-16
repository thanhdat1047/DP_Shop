using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Users;
using DP_Shop.Helpers.Query;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface IUserRespository
    {
        Task<ApplicationUser> GetUserbyUsername(string username);
        Task<List<UpdateUserRequestDto>> GetUsers(QueryUser query);
        Task<ApplicationUser> GetUserById(string id);
        Task<Result<UserDto>> GetUserProfile(string id);
        Task<Result<ApplicationUser>> UpdateAsync(string id, UpdateUserRequestDto userDto);
        Task<Result<ApplicationUser>> DeleteAsync(string id);
        Task<bool> UserExists(string id);

    }
}
