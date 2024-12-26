using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Users;

namespace DP_Shop.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this ApplicationUser applicationUser)
        {
            return new UserDto
            {
                Id = applicationUser.Id,
                Username = applicationUser.UserName,
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber
            };
        }
        public static UserProfile ToUserProfile(this ApplicationUser applicationUser)
        {
            return new UserProfile
            {
                Id = applicationUser.Id,
                Username = applicationUser.UserName,
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber
            };
        }
        public static ApplicationUser ToAplicationUser(this UserDto userDto)
        {
            return new ApplicationUser
            {
                Id = userDto.Id,
                UserName = userDto.Username,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber
            };
        }
    }
}
