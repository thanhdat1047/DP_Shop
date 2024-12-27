using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Enum;
using DP_Shop.DTOs.Products;
using DP_Shop.DTOs.Users;
using DP_Shop.Interface;
using DP_Shop.Models;
using DP_Shop.Models.Result;
using DP_Shop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Security.Cryptography;

namespace DP_Shop.Respository
{
    public class AccountRespository : IAccountRespository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _dbContext;

        public AccountRespository(AppDbContext dbContext,  ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _dbContext = dbContext;
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

        public async Task<Result<bool>> AssignRole(UserRole userRole)
        {
            IDbContextTransaction? transaction = null;
            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();

                var user = await _userManager.FindByNameAsync(userRole.Username);

                if (user == null)
                {
                    return new Result<bool>("User not found");
                }

                if (await IsExistsRole(user, userRole))
                {
                    return new Result<bool>("User already has this role.");
                }
                if (!await RemoveRole(user, userRole))
                {
                    return new Result<bool>("Failed to remove old role.");
                }

                var result = await _userManager.AddToRoleAsync(user, userRole.Role.ToString());

                if (!result.Succeeded)
                {
                    return new Result<bool>("Couldn't assigned this role"); ;
                }

                await transaction.CommitAsync();
                return new Result<bool>(true);
            }
            catch (Exception ex) 
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
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
        public async Task<Result<Token>> GenerateAccessToken(RefreshTokenRequest model)
        {
            if(model == null)
            {
                return new Result<Token>("Request model cannot be null.");
            }

            if(string.IsNullOrEmpty(model.Username))
            {
                return new Result<Token>("Username is required.");
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return new Result<Token>("User not found.");
            }
            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole == null || !userRole.Any()) 
            {
                return new Result<Token>("User does not have any roles assigned.");
            }
            try
            {
                var token = new Token { AccessToken = _tokenService.CreateToken(user, userRole) };
                return new Result<Token>(token);
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<Token>(errorMessage);
            }
        }
        public async Task<Result<bool>> Register(Register model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email }; 
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) {

                var userRole = new UserRole
                {
                    Username = user.UserName,
                    Role = Role.User
                };
                var assignedRoleResult = await AssignRole(userRole);

                if (assignedRoleResult.Succeeded)
                {
                    return new Result<bool>(true);
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    return new Result<bool>("Couldn't assigned role");
                }
            }
            var errorMessages = string.Join(",", result.Errors.Select(e => e.Description));
            return new Result<bool>(errorMessages);

        }

        public async Task<bool> RemoveRole(ApplicationUser user, UserRole userRole)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());
            if(removeResult.Succeeded) return true;
            return false;
        }
        public async Task<bool> IsExistsRole(ApplicationUser user, UserRole userRole)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Contains(userRole.Role.ToString()))
            {
                return true;
            }
            return false;
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public async Task SaveRefreshTokenAsync(string username, string refreshToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); 
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<bool> ValidateRefreshTokenAsync(string username, string refreshToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null && user.RefreshToken == refreshToken && user.RefreshTokenExpiryTime > DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }

        public async Task<Result<bool>> Logout(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new Result<bool>("User not found.");
            }
            try
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = DateTime.MinValue;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new Result<bool>($"Failed to update user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                // Log lỗi và inner exception
                var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return new Result<bool>($"Error occurred while logging out: {innerException}");
            }
        }
    }
}
