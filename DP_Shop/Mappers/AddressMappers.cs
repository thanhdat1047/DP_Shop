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
                WardCode = address.WardCode,
                Detail = address.Detail,
            };
        }
        public static AddressDto ToAddressDto(this Address address)
        {
            return new AddressDto
            {
                Id = address.Id,    
                WardCode = address.WardCode,
                Detail = address.Detail
            };
        }
        public static AddressModel ToAddressModel(this Address address, string path)
        {
            return new AddressModel
            {
                WardCode = address.WardCode,
                Detail = address.Detail,
                Path_With_Type = path   
            };
        }

        public static AddressModelResponse ToResponseAddressModel(this Address address)
        {
            return new AddressModelResponse
            {
                Id = address.Id,    
                Detail = address.Detail,
                WardCode = address.WardCode,
            };
        }
        public static AddressModelResponse ToResponseAddressModel(this Address address, string path)
        {
            return new AddressModelResponse
            {
                Id = address.Id,
                Detail = address.Detail,
                WardCode = address.WardCode,
                Path_With_Type = path
            };
        }
        public static UserAddressResponse ToUserAddressResponse(this Address addressModel, bool isDefault)
        {
            return new UserAddressResponse
            {
                Id = addressModel.Id,
                WardCode = addressModel.WardCode,
                Detail = addressModel.Detail,
                isDefault = isDefault  ,
            };
        }
        public static Address ToAddress (this AddressModel addressModel)
        {
            return new Address
            {
                WardCode = addressModel.WardCode,
                Detail = addressModel.Detail,
            };
        }

        
    }
}
