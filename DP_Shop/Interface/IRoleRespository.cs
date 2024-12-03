using DP_Shop.DTOs.Enum;

namespace DP_Shop.Interface
{
    public interface IRoleRespository
    {
        Task<bool> AddRoleAsync(Role role);
        Task<bool> RoleExistsAsync(Role role);
    }
}
