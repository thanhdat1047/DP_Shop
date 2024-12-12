using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Users;
using DP_Shop.Models;
using DP_Shop.Models.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Interface
{
    public interface IAccountRespository
    {
        public Task<Result<Boolean>> Register(Register register);
        public Task<String> Login(Login login);
        public Task<Boolean> AddRole(string role);
        public Task<Result<Boolean>> AssignRole(UserRole userRole);
        public Task<Boolean> RemoveRole(ApplicationUser user, UserRole userRole);
        public Task<Boolean> IsExistsRole(ApplicationUser user, UserRole userRole);
        public string GenerateRefreshToken();
        public Task SaveRefreshTokenAsync(string username, string refreshToken);
        public Task<bool> ValidateRefreshTokenAsync(string username, string refreshToken);
        public Task<Result<Token>> GenerateAccessToken(RefreshTokenRequest model);
        public Task<Result<bool>> Logout(string username);
    }
}
