using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.Products;
using DP_Shop.Helpers.Query;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface IProductRespository
    {
        Task<Result<ProductResponse>> GetById(int id);
        Task<Result<ProductResponse>> GetBySlug(string slug);
        Task<Result<List<ProductResponse>>> GetAll(QueryProducts query);
        Task<Result<List<ProductResponse>>> GetProductByCategoryId(QueryProducts query, int cateId);
        Task<Result<ProductDtoResponse>> CreateAsync(ProductWithImagesRequest model);
        Task<Result<ProductDto>> UpdateAsync(int id, UpdateProductRequest model);
        Task<Result<Boolean>> DeleteById(int id);
        Task<Result<List<ProductResponse>>> GetSoftDeletedList(QueryProducts query); 
        Task<Result<Boolean>> SoftDeleteAsync(int id);
        Task<Result<Boolean>> RestoreAsync(int id);
        Task<Boolean> ProductExists(int id);
        Task<Result<List<ProductResponse>>> GetProductsWithFilters(QueryProducts query, Func<IQueryable<Product>, IQueryable<Product>>? filter = null);
        Task<Result<List<ProductResponse>>> GetExpiringProducts( QueryProducts query, int daysBeforeExpiry = 7, Func<IQueryable<Product>, IQueryable<Product>>? filter = null);


    }
}
