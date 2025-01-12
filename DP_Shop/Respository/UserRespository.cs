using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Users;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DP_Shop.Respository
{
    public class UserRespository : IUserRespository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;
        public UserRespository(UserManager<ApplicationUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext; 
        }

        public async Task<Result<bool>> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new Result<bool>("User not found");
                }

                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return new Result<bool>($"Password change failed: {errors}");
                }

                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }

        public async Task<Result<ApplicationUser>> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) {
                return new Result<ApplicationUser>("User not found");
            }
            var result = await _userManager.DeleteAsync(user);

            if(!result.Succeeded)
            {
                var errorMessages = string.Join(",", result.Errors.Select(e => e.Description));
                return new Result<ApplicationUser>(errorMessages);
            }
            return new Result<ApplicationUser>(user);
        }

        /* public async Task<ApplicationUser> GetUserById(string id)
         {
             var user = await _userManager.FindByIdAsync(id);
             if (user == null)
             {
                 throw new Exception("User not found");
             }
             return user;
         }*/

        public async Task<Result<UserResponse>> GetUserById(string id)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(u => u.UserAddresses)
                    .ThenInclude(au => au.Address)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return new Result<UserResponse>("User not found");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userResponse = user.ToUserResponse();

                userResponse.Roles = roles.ToList();

                userResponse.Addresses = user.UserAddresses?
                    .Where(ua => ua.Address != null)
                    .Select(ua => ua.Address!.ToAddressModel())
                    .ToList() ?? new List<AddressModel>();

                return new Result<UserResponse>(userResponse);

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<UserResponse>(errorMessage);
            }
        }
        public async Task<Result<UserResponse>> GetUserbyUsername(string username)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(u => u.UserAddresses)
                    .ThenInclude(au => au.Address)
                    .FirstOrDefaultAsync(u => u.UserName == username );

                if (user == null)
                {
                    return new Result<UserResponse>("User not found");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userResponse = user.ToUserResponse();

                userResponse.Roles = roles.ToList();

                userResponse.Addresses = user.UserAddresses?
                    .Where(ua => ua.Address != null)
                    .Select(ua => ua.Address!.ToAddressModel())
                    .ToList() ?? new List<AddressModel>();

                return new Result<UserResponse>(userResponse);

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<UserResponse>(errorMessage);
            }
        }
       /* public async Task<ApplicationUser> GetUserbyUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }*/

        public async Task<Result<UserProfile>> GetUserProfile(string id)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(u => u.UserAddresses) 
                    .ThenInclude(au => au.Address)
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return new Result<UserProfile>("User not found");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var userDto = user.ToUserProfile();

                userDto.Roles = roles.ToList();

                userDto.Addresses = user.UserAddresses?
                    .Where(ua => ua.Address != null)
                    .Select(ua =>  ua.Address!.ToAddressDto())
                    .ToList() ?? new List<AddressDto>();

                var wardCodes = userDto.Addresses.Select(a => a.WardCode).Distinct().ToList();
                var wards = await _dbContext.Wards
                    .AsNoTracking()
                    .Where(w => wardCodes.Contains(w.Code))
                    .ToDictionaryAsync(w => w.Code, w => w.Path_With_Type);

                foreach (var address in userDto.Addresses)
                {
                    address.Path_With_Type = wards.ContainsKey(address.WardCode)
                        ? wards[address.WardCode]
                        : "";

                    var isDeault = await _dbContext.UserAddresses.AnyAsync(ua => ua.AddressId == address.Id && ua.IsDefault == true);
                    if (isDeault)
                    {
                        address.isDefault = true;
                    }
                }

                return new Result<UserProfile>(userDto);

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<UserProfile>(errorMessage);
            }
        }

        public async Task<Result<List<UserResponse>>> GetUsers(QueryUser query)
        {
            try
            {
                var users = _dbContext.Users
                    .Include(u => u.UserAddresses)
                    .ThenInclude(ua => ua.Address)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(query.UserName))
                {
                    users = users.Where(u => u.UserName!.Contains(query.UserName));
                }

                if (!string.IsNullOrEmpty(query.SortBy))
                {
                    switch (query.SortBy.ToLower())
                    {
                        case "username":
                            users = query.isDecsending ? users.OrderByDescending(u => u.UserName) : users.OrderBy(u => u.UserName);
                            break;
                        case "email":
                            users = query.isDecsending ? users.OrderByDescending(u => u.Email) : users.OrderBy(u => u.Email);
                            break;
                        default:
                            users = users.OrderBy(u => u.UserName);
                            break;

                    }
                }
                else
                {
                    users = users.OrderBy(u => u.UserName);
                }

                var skip = (query.PageNumber - 1) * query.PageSize;
                var pagedUsers = await users.Skip(skip).Take(query.PageSize).ToListAsync();

                var updateUserDto = new List<UserResponse>();
                foreach (var user in pagedUsers)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var addresses = user.UserAddresses?
                        .Where(ua => ua.Address != null)
                        .Select(ua => ua.Address!.ToAddressModel())
                        .ToList() ?? new List<AddressModel>();

                    updateUserDto.Add(new UserResponse
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Username = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        Roles = roles.ToList(),
                        Addresses = addresses,

                    });
                }
                return new Result<List<UserResponse>>(updateUserDto);
            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<UserResponse>>(errorMessage);
            }
            
        }

        public async Task<Result<ApplicationUser>> UpdateAsync(string id, UpdateUserRequestDto userDto)
        {
            var user = await _userManager.FindByIdAsync(id);    
            if(user == null)
            {
                return new Result<ApplicationUser>("User not found");
            }
            user.UserName = userDto.Username;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;

            var updateResult = await _userManager.UpdateAsync(user);
            if(!updateResult.Succeeded) 
            {
                 var errorMessages = string.Join(",", updateResult.Errors.Select(e => e.Description));
                 return new Result<ApplicationUser>(errorMessages);
            }
            return new Result<ApplicationUser>(user);
        }

        public async Task<bool> UserExists(string id)
        {
            return await _userManager.Users.AnyAsync(u => u.Id == id);
        }


    }
}
