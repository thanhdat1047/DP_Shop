using DP_Shop.Data.Entities;

namespace DP_Shop.Interface
{
    public interface IUserRespository
    {
        Task<ApplicationUser> GetUserbyUsername(string username);

    }
}
