using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Address;
using DP_Shop.Helpers.Query;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface IAddressRepository
    {
        Task<Result<AddressModel>> GetAddressByIdAsyncNoTracking(int id);
        Task<Result<List<UserAddressResponse>>> GetAddressesByUserId(QueryAddress query, string userId);
        Task<List<AddressModelResponse>> GetAllAddress(QueryAddress query);
        Task<Result<AddressModel>> CreateAsync(AddressRequest address, string userId, bool isDefault);
        Task<Result<AddressModel>> UpdateAsync(int id, UpdateAddressRequest address, string userId); 
        //Task<Result<bool>> UnlinkToAddressAsync(int id, string userId);
        Task<Result<bool>> DeleteAsync(int id, string userId);
        Task<Boolean> AddressExists(int id);
        Task<Boolean> UserAddressExists(string userId);

        Task<Result<List<Provinces>>> GetProvinces();
        Task<Result<ProvinceDto>> GetProvinceByCode(string code);
        Task<Result<List<District>>> GetDistricstByParentCode(string parentCode);
        Task<Result<List<Ward>>> GetWardsByParentCode(string parentCode);
    }
}
