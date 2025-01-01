using DP_Shop.DTOs.OrderProduct;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface IOrderProductRespository
    {
        Task<Result<OrderProductResponse>> GetOrderProductById(int id);  

    }
}
