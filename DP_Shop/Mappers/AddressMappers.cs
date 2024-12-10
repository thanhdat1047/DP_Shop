using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Users;
using System.Net;

namespace DP_Shop.Mappers
{
    public static class AddressMappers
    {
        public static AddressModel ToAddressModel(this Address address)
        {
            return new AddressModel
            {
                City = address.City,
                Code = address.Code,
            };
        }

        public static AddressModelResponse ToResponseAddressModel(this Address address)
        {
            return new AddressModelResponse
            {
                Id = address.Id,    
                City = address.City,
                Code = address.Code,
            };
        }
        public static UserAddressResponse ToUserAddressResponse(this Address addressModel, bool isDefault)
        {
            return new UserAddressResponse
            {
                Id = addressModel.Id,
                City = addressModel.City,
                Code = addressModel.Code,
                isDefault = isDefault  ,
            };
        }
        public static Address ToAddress (this AddressModel addressModel)
        {
            return new Address
            {
                City = addressModel.City,
                Code = addressModel.Code,
            };
        }

        
    }
}
