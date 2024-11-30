using DP_Shop.Data.Entities;
using DP_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Interface
{
    public interface IAccountRespository
    {
        public Task<Boolean> Register(Register model);
        public Task<String> Login(Login model);
        public Task<Boolean> AddRole(string role);
        public Task<Boolean> AssignRole(ApplicationUser model, UserRole userRole);

    }
}
