using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Users;
using DP_Shop.Helpers.Query;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface IUserRespository
    {
        Task<Result<UserResponse>> GetUserbyUsername(string username);
        Task<Result<List<UserResponse>>> GetUsers(QueryUser query);
        Task<Result<UserResponse>> GetUserById(string id);
        Task<Result<UserResponse>> GetUserProfile(string id);
        Task<Result<ApplicationUser>> UpdateAsync(string id, UpdateUserRequestDto userDto);
        Task<Result<ApplicationUser>> DeleteAsync(string id);
        Task<Result<Boolean>> ChangePassword(string userId, string currentPassword, string newPassword);
        Task<bool> UserExists(string id);

    }
}
