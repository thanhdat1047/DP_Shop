using DP_Shop.Data;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Products;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Microsoft.EntityFrameworkCore;

namespace DP_Shop.Respository
{
    public class ProductRespository : IProductRespository
    {
        private readonly AppDbContext _dbContext;
        public ProductRespository(AppDbContext dbContext)
        {
            _dbContext = dbContext; 

        }
        public async Task<Result<ProductDto>> CreateAsync(CreateProductRequest model)
        {
            try
            {
                if(model.ExpiryDate < DateTime.Now)
                {
                    return new Result<ProductDto>("The expiration date must be after the current date.");
                }
                if(!await _dbContext.Categories.AnyAsync(c => c.Id == model.CategoryId))
                {
                    return new Result<ProductDto>("Category not found");
                }

                var newProduct = model.ToProduct();
                await _dbContext.Products.AddAsync(newProduct);
                await _dbContext.SaveChangesAsync();

                return new Result<ProductDto>(newProduct.ToProductDto());   

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ProductDto>(errorMessage);
            }
        }

        public Task<Result<bool>> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<ProductDto>>> GetAll(QueryProducts query)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ProductDto>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<ProductDto>>> GetSoftDeletedList()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _dbContext.Products.AnyAsync();
        }

        public Task<Result<bool>> RestoreAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ProductDto>> UpdateAsync(int id, ProductDto ProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
