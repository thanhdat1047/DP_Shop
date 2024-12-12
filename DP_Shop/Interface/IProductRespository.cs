using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Products;
using DP_Shop.Helpers.Query;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface IProductRespository
    {
        Task<Result<ProductDto>> GetById(int id);
        Task<Result<List<ProductDto>>> GetAll(QueryProducts query);
        Task<Result<ProductDto>> CreateAsync(CreateProductRequest model);
        Task<Result<ProductDto>> UpdateAsync(int id, ProductDto ProductDto);
        Task<Result<Boolean>> DeleteById(int id);
        Task<Result<List<ProductDto>>> GetSoftDeletedList(); 
        Task<Result<Boolean>> SoftDeleteAsync(int id);
        Task<Result<Boolean>> RestoreAsync(int id);
        Task<Boolean> ProductExists(int id);



    }
}
