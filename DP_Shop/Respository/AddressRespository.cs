using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Address;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DP_Shop.Respository
{
    public class AddressRespository : IAddressRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public AddressRespository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {

            _dbContext = dbContext;
            _userManager = userManager; 

        }


        public async Task<Result<AddressModel>> CreateAsync(AddressModel address, string userId, bool isDefault)
        {
            IDbContextTransaction? transaction = null;
            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();

                var newAddress = address.ToAddress();



                await _dbContext.Addresses.AddAsync(newAddress);
                await _dbContext.SaveChangesAsync();

                var newAddressId = newAddress.Id;

                if (newAddressId == 0)
                {
                    return new Result<AddressModel>("Address creation failed. Invalid address ID.");
                }

                var userExists = await _dbContext.Users.AnyAsync(u => u.Id == userId);
                if (!userExists)
                {
                    return new Result<AddressModel>("User not found.");
                }
                if(isDefault)
                {
                    var addressDefault = await _dbContext.UserAddresses.FirstOrDefaultAsync(ua => ua.UserId == userId && isDefault == true);
                    if (addressDefault != null)
                    {
                        addressDefault.IsDefault = false;
                        await _dbContext.SaveChangesAsync();    
                    }
                }
               
                var userAddress = new UserAddress
                {
                    AddressId = newAddressId,
                    IsDefault = isDefault,
                    UserId = userId,
                };

                await _dbContext.UserAddresses.AddAsync(userAddress);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return new Result<AddressModel>(address);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<AddressModel>(errorMessage);
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var address = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == id);
                if (address == null)
                {
                    return new Result<bool>("Address not found");
                }

                var isAnyAddressUser = await _dbContext.UserAddresses.AnyAsync(au => au.AddressId == id);
                if (isAnyAddressUser) 
                {
                    return new Result<bool>("The address cannot be deleted because there is a user associated with this address");
                }

                _dbContext.Addresses.Remove(address);
                await _dbContext.SaveChangesAsync();

                return new Result<bool>(true);

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }


        public async Task<Result<bool>> UnlinkToAddressAsync(int id, string userId)
        {
            try
            {
                var userAddress = await _dbContext.UserAddresses
                    .FirstOrDefaultAsync(ua => ua.AddressId == id && ua.UserId == userId);
                if (userAddress == null)
                {
                    return new Result<bool>("Address not found");
                }
                if (!userAddress.IsDefault)
                {
                    return new Result<bool>("Address is default");
                }
                _dbContext.UserAddresses.Remove(userAddress);
                await _dbContext.SaveChangesAsync();

                return new Result<bool>(true);
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }

        public async Task<Result<AddressModel>> GetAddressByIdAsyncNoTracking(int id)
        {
            try
            {
                var address = await _dbContext.Addresses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id);
                if(address == null)
                {
                    return new Result<AddressModel>("Address not found");
                }
                return new Result<AddressModel>(address.ToAddressModel());  
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<AddressModel>(errorMessage);
            }

        }
        public async Task<List<AddressModelResponse>> GetAllAddress(QueryAddress query)
        {
            var addresses = _dbContext.Addresses.AsQueryable();
            if (!string.IsNullOrEmpty(query.City))
            {
                addresses = addresses.Where(a => a.City.Contains(query.City));  
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch(query.SortBy.ToLower())
                {
                    case "city":
                        addresses = query.isDecsending ? addresses.OrderByDescending(a => a.City) : addresses.OrderBy(a => a.City);
                        break;
                    case "code":
                        addresses = query.isDecsending ? addresses.OrderByDescending(a => a.Code) : addresses.OrderBy(a => a.Code);
                        break;
                    default:
                        addresses = addresses.OrderBy(a => a.City);
                        break;
                }
            }
            else
            {
                addresses = addresses.OrderBy( a => a.City);
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            var pageAddress = await addresses.Skip(skip).Take(query.PageSize).ToListAsync();

            var listAddressDto = new List<AddressModelResponse>(); 
            foreach(var address in pageAddress)
            {
                listAddressDto.Add(address.ToResponseAddressModel());   
            }
            return listAddressDto;  

        }

        public async Task<Result<List<UserAddressResponse>>> GetAddressesByUserId(QueryAddress query, string userId)
        {
            try
            {
                var userAddresses = await _dbContext.UserAddresses
                    .Where(ua => ua.UserId == userId)
                    .Include(ua => ua.Address)
                    .ToListAsync();

                if(userAddresses == null || !userAddresses.Any())
                {
                    return new Result<List<UserAddressResponse>>("No addresses found for this user");
                }
                var addressModel = userAddresses
                    .Select(ua => ua.Address.ToUserAddressResponse(ua.IsDefault)).AsQueryable();

                if (!string.IsNullOrEmpty(query.City))
                {
                    addressModel = addressModel.Where(a => a.City.Contains(query.City));
                }
                if (!string.IsNullOrWhiteSpace(query.SortBy))
                {
                    switch (query.SortBy.ToLower())
                    {
                        case "city":
                            addressModel = query.isDecsending ? addressModel.OrderByDescending(a => a.City) : addressModel.OrderBy(a => a.City);
                            break;
                        case "code":
                            addressModel = query.isDecsending ? addressModel.OrderByDescending(a => a.Code) : addressModel.OrderBy(a => a.Code);
                            break;
                        default:
                            addressModel = addressModel.OrderBy(a => a.City);
                            break;
                    }
                }
                else
                {
                    addressModel = addressModel.OrderBy(a => a.City);
                }

                var skip = (query.PageNumber - 1) * query.PageSize;
                var pageAddress = addressModel.Skip(skip).Take(query.PageSize).ToList();

                return new Result<List<UserAddressResponse>>(pageAddress);

            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<UserAddressResponse>>(errorMessage);
            }
        }

        public async Task<Result<AddressModel>> UpdateAsync(int id, UpdateAddressRequest model, string userId)
        {
            IDbContextTransaction? transaction = null;
            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();

                if (model.IsDefault)
                {
                    var oldAddressDefault = await _dbContext.UserAddresses
                        .SingleOrDefaultAsync(ua => ua.UserId == userId && ua.IsDefault == true && ua.AddressId != id);
                   
                    if (oldAddressDefault != null)
                    {
                        oldAddressDefault.IsDefault = false;

                        var newAddressDefault = await _dbContext.UserAddresses
                            .SingleOrDefaultAsync(ua => ua.UserId == userId && ua.AddressId == id);
                        
                        if (newAddressDefault != null)
                        {
                            newAddressDefault.IsDefault = true;
                        }
                    }

                   
                }
                var updateAddress = await _dbContext.Addresses
                    .SingleOrDefaultAsync(a => a.Id == id);

                if (updateAddress == null)
                {
                    return new Result<AddressModel>("Address not found");
                }

                updateAddress.City = model.Address.City;
                updateAddress.Code = model.Address.Code;   

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return new Result<AddressModel>(updateAddress.ToAddressModel());
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<AddressModel>(errorMessage);
            }
        }


        public async Task<bool> AddressExists(int id)
        {
            return await _dbContext.Addresses
                .AnyAsync(a => a.Id == id );
                
        }
    }
}