using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Address;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;
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

        public async Task<Result<AddressModel>> CreateAsync(AddressRequest address, string userId, bool isDefault)
        {
            IDbContextTransaction? transaction = null;
            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();

                if(userId == null)
                {
                    return new Result<AddressModel>("UserId is invalid.");
                }

                var userExists = await _dbContext.Users
                    .AsNoTracking()
                    .AnyAsync(u => u.Id == userId);
                if (!userExists )
                {
                    return new Result<AddressModel>("User not found.");
                }


                var ward = await _dbContext.Wards
                    .AsNoTracking()
                    .SingleOrDefaultAsync(w => w.Code == address.WardCode);
                if (ward == null)
                {
                    return new Result<AddressModel>("Ward not found.");
                }


                if (isDefault)
                {
                    var currentDefaultAddress = await _dbContext.UserAddresses
                        .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.IsDefault);

                    if (currentDefaultAddress != null)
                    {
                        currentDefaultAddress.IsDefault = false;
                        _dbContext.UserAddresses.Update(currentDefaultAddress);
                        //await _dbContext.SaveChangesAsync();
                    }
                }

                var newAddress = new Address
                {
                    Detail = address.Detail,
                    WardCode = address.WardCode,
                    UserAddresses = new List<UserAddress>
                    {
                        new UserAddress
                        {
                            IsDefault = isDefault,
                            UserId = userId,
                        }
                    }
                };

                
                await _dbContext.Addresses.AddAsync(newAddress);
                await _dbContext.SaveChangesAsync();

                var addressResponse = newAddress.ToAddressModel();

                await transaction.CommitAsync();

                if (ward != null)
                {
                    addressResponse.Path_With_Type = ward.Path_With_Type;
                }
                return new Result<AddressModel>(addressResponse);
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
            finally
            {
                if (transaction != null)
                {
                    await transaction.DisposeAsync();
                }
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id, string userId)
        {
            try
            {
                var addressUser = await _dbContext.UserAddresses
                    .AsNoTracking()
                    .Where(ua => ua.AddressId == id && ua.UserId == userId)
                    .SingleOrDefaultAsync();

                if (addressUser == null)
                {
                    return new Result<bool>("Address not found");
                }

                if (addressUser.IsDefault)
                {
                    return new Result<bool>("Address is default");
                }

                var address = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == id);

                if (address == null)
                {
                    return new Result<bool>("Address not found");
                }

                _dbContext.UserAddresses.Remove(addressUser);
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


        /*public async Task<Result<bool>> UnlinkToAddressAsync(int id, string userId)
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
        }*/

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
                var addressModel = address.ToAddressModel();

                var ward = await _dbContext.Wards
                    .AsNoTracking()
                    .SingleOrDefaultAsync(w => w.Code == address.WardCode);
                if (ward != null)
                {
                    addressModel.Path_With_Type = ward.Path_With_Type;
                }

                return new Result<AddressModel>(addressModel);  
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<AddressModel>(errorMessage);
            }

        }
        public async Task<List<AddressModelResponse>> GetAllAddress(QueryAddress query)
        {
            var addresses = _dbContext.Addresses
                .AsQueryable()
                .AsNoTracking();
            if (!string.IsNullOrEmpty(query.Detail))
            {
                addresses = addresses.Where(a => a.Detail.Contains(query.Detail));  
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch(query.SortBy.ToLower())
                {
                    case "detail":
                        addresses = query.isDecsending ? addresses.OrderByDescending(a => a.Detail) : addresses.OrderBy(a => a.Detail);
                        break;
                    case "wardcode":
                        addresses = query.isDecsending ? addresses.OrderByDescending(a => a.WardCode) : addresses.OrderBy(a => a.WardCode);
                        break;
                    default:
                        addresses = addresses.OrderBy(a => a.WardCode);
                        break;
                }
            }
            else
            {
                addresses = addresses.OrderBy( a => a.WardCode);
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            var pageAddress = await addresses.Skip(skip).Take(query.PageSize).ToListAsync();

            var listAddressDto = new List<AddressModelResponse>(); 
            foreach(var address in pageAddress)
            {
                var addressResponse = new AddressModelResponse();
                var ward = await _dbContext.Wards
                    .AsNoTracking()
                    .SingleOrDefaultAsync(w => w.Code == address.WardCode);
                if (ward != null)
                {
                    addressResponse = address.ToResponseAddressModel(ward.Path_With_Type);
                }
                listAddressDto.Add(addressResponse);   
            }
            return listAddressDto;  

        }

        public async Task<Result<List<UserAddressResponse>>> GetAddressesByUserId(QueryAddress query, string userId)
        {
            try
            {
                var userAddresses = await _dbContext.UserAddresses
                    .AsNoTracking()
                    .Where(ua => ua.UserId == userId)
                    .Include(ua => ua.Address)
                    .ToListAsync();

                if(userAddresses == null || !userAddresses.Any())
                {
                    return new Result<List<UserAddressResponse>>("No addresses found for this user");
                }
                var addressModel = userAddresses
                    .Where(ua => ua.Address != null)
                    .Select(ua => ua.Address!.ToUserAddressResponse(ua.IsDefault))
                    .AsQueryable();

                if (!string.IsNullOrEmpty(query.Detail))
                {
                    addressModel = addressModel.Where(a => a.Detail.Contains(query.Detail));
                }
                if (!string.IsNullOrWhiteSpace(query.SortBy))
                {
                    switch (query.SortBy.ToLower())
                    {
                        case "city":
                            addressModel = query.isDecsending ? addressModel.OrderByDescending(a => a.Detail) : addressModel.OrderBy(a => a.Detail);
                            break;
                        case "wardcode":
                            addressModel = query.isDecsending ? addressModel.OrderByDescending(a => a.WardCode) : addressModel.OrderBy(a => a.WardCode);
                            break;
                        default:
                            addressModel = addressModel.OrderBy(a => a.WardCode);
                            break;
                    }
                }
                else
                {
                    addressModel = addressModel.OrderBy(a => a.WardCode);
                }

                var skip = (query.PageNumber - 1) * query.PageSize;
                var pageAddress = addressModel.Skip(skip).Take(query.PageSize).ToList();

                foreach (var address in pageAddress)
                {
                    var ward = await _dbContext.Wards
                        .AsNoTracking()
                        .SingleOrDefaultAsync(w => w.Code == address.WardCode);
                    if (ward != null)
                    {
                        address.Path_With_Type = ward.Path_With_Type;
                    }
                }
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

                updateAddress.Detail = model.Address.Detail;
                updateAddress.WardCode = model.Address.WardCode;

               
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                var addressModel = updateAddress.ToAddressModel();
                var ward = await _dbContext.Wards
                .AsNoTracking()
                        .SingleOrDefaultAsync(w => w.Code == updateAddress.WardCode);
                if (ward != null)
                {
                    addressModel.Path_With_Type = ward.Path_With_Type;
                }

                return new Result<AddressModel>(addressModel);
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

        public async Task<Result<List<Provinces>>> GetProvinces()
        {
            try
            {
                var provinces = await _dbContext.Provinces
                    .AsNoTracking()
                    .ToListAsync();
                return new Result<List<Provinces>>(provinces);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<Provinces>>(errorMessage);
            }
        }

        public async Task<Result<ProvinceDto>> GetProvinceByCode(string code)
        {
            try
            {
                var province = await _dbContext.Provinces
                    .AsNoTracking()
                    .Where(p => p.Code == code)
                        .Include(p => p.Districts)
                    .SingleOrDefaultAsync();

                if (province == null)
                {
                    return new Result<ProvinceDto>("Provinces not found");
                }

                var provinceDto = new ProvinceDto()
                {
                    Code = province.Code,
                    Name = province.Name,
                    Type = province.Type,
                    Name_With_Type = province.Name_With_Type,
                    Slug = province.Slug,
                    Districts = province.Districts != null ?
                        province.Districts.Select(d => new District
                        {
                            Code = d.Code,
                            Name = d.Name,
                            Type = d.Type,
                            Name_With_Type = d.Name_With_Type,
                            Path = d.Path,
                            Path_With_Type = d.Path_With_Type,
                            Slug = d.Slug,
                            ParentCode = d.ParentCode

                        }).ToList() : new List<District>()
                };
                return new Result<ProvinceDto>(provinceDto);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ProvinceDto>(errorMessage);
            }
        }

        public async Task<Result<List<District>>> GetDistricstByParentCode(string parentCode)
        {
            try
            {
                var isExistsProvince = await _dbContext.Provinces
                    .AsNoTracking()
                    .AnyAsync(p => p.Code ==  parentCode);  

                if(!isExistsProvince)
                {
                    return new Result<List<District>>("Provinces isn't exists");
                }

                var district = await _dbContext.Districts
                    .AsNoTracking()
                    .Where(d => d.ParentCode == parentCode)
                    .ToListAsync();

                return new Result<List<District>>(district);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<District>>(errorMessage);
            }
        }

        public async Task<Result<List<Ward>>> GetWardsByParentCode(string parentCode)
        {
            try
            {
                var isExistsDistricts = await _dbContext.Districts
                    .AsNoTracking()
                    .AnyAsync(d => d.Code == parentCode);

                if (!isExistsDistricts)
                {
                    return new Result<List<Ward>>("Dicstrict isn't exists");
                }

                var wards = await _dbContext.Wards
                    .AsNoTracking()
                    .Where(w => w.ParentCode == parentCode)
                    .ToListAsync();

                return new Result<List<Ward>>(wards);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<Ward>>(errorMessage);
            }
        }

        public async Task<bool> UserAddressExists(string userId)
        {
            try
            {
                var checkUserAddresses = await _dbContext.UserAddresses
                .AsNoTracking()
                .AnyAsync(ua => ua.UserId == userId );
                return checkUserAddresses;
            }
            catch { return false; } 

        }
    }
}