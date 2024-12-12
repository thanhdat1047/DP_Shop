using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Categories;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface ICategoryRespository
    {
        Task<Result<List<CategoryDto>>> GetAll();
        Task<Result<List<CategoryDto>>> GetSoftDeletedList();
        Task<Result<CategoryDto>> GetById(int id);
        Task<Result<CategoryDto>> CreateAsync(CreateCategoryRequest createCategory);
        Task<Result<CategoryDto>> UpdateAsync(int id, CategoryDto categoryDto);
        Task<Result<Boolean>> DeleteAsync(int id);
        Task<Result<Boolean>> SoftDeleteAsync(int id);
        Task<Result<Boolean>> RestoreAsync(int id);
        Task<Boolean> CategoryExists(int id);

    }
}
