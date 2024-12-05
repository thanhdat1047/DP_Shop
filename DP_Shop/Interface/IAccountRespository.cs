using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Result;
using DP_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Interface
{
    public interface IAccountRespository
    {
        public Task<Result<Boolean>> Register(Register register);
        public Task<String> Login(Login login);
        public Task<Boolean> AddRole(string role);
        public Task<Boolean> AssignRole(ApplicationUser user, Role role);
        public Task<Boolean> RemoveRole(ApplicationUser user, UserRole userRole);
        public Task<Boolean> IsExistsRole(ApplicationUser user, UserRole userRole);

    }
}
