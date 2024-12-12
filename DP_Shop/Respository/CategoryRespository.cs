using DP_Shop.Data;
using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Categories;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Microsoft.EntityFrameworkCore;

namespace DP_Shop.Respository
{
    public class CategoryRespository : ICategoryRespository
    {
        private readonly AppDbContext _context;
        public CategoryRespository(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<Result<CategoryDto>> CreateAsync(CreateCategoryRequest createCategory)
        {
            try
            {
                var newCategory = createCategory.ToCategory();
                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();

                return new Result<CategoryDto>(newCategory.ToCategoryDto());
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<CategoryDto>(errorMessage);
            }
        }

        

        public async Task<Result<List<CategoryDto>>> GetAll()
        {
            try
            {
                var categories = await _context.Categories.Where(c => c.DeletedAt == null).ToListAsync();
                var categoriesSto = categories.Select(c => c.ToCategoryDto()).ToList();
                return new Result<List<CategoryDto>>(categoriesSto);
            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<CategoryDto>>(errorMessage);
            }
        }

        public async Task<Result<CategoryDto>> GetById(int id)
        {
            try
            {
                var category = await _context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id); 
                if(category == null)
                {
                    return new Result<CategoryDto>("Category not found");
                }
                return new Result<CategoryDto>(category.ToCategoryDto());

            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<CategoryDto>(errorMessage);
            }
        }

        public async Task<Result<CategoryDto>> UpdateAsync(int id, CategoryDto categoryDto)
        {
            try
            {
                var category = await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    return new Result<CategoryDto>("Category not found");
                }

                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;

                await _context.SaveChangesAsync();
                return new Result<CategoryDto>(category.ToCategoryDto());
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<CategoryDto>(errorMessage);
            }
        }
        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories
                .AnyAsync(c => c.Id==id);
        }
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    return new Result<bool>("Category not found");
                }
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return new Result<bool>(true);

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }
        public async Task<Result<bool>> SoftDeleteAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    return new Result<bool>("Category not found");
                }
                category.DeletedAt = DateTime.Now;   
                await _context.SaveChangesAsync();

                return new Result<bool>(true);

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }

        public async Task<Result<Boolean>> RestoreAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    return new Result<bool>("Category not found");
                }
                category.DeletedAt = null;
                await _context.SaveChangesAsync();

                return new Result<bool>(true);

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }
        public async Task<Result<List<CategoryDto>>> GetSoftDeletedList()
        {
            try
            {
                var categories = await _context.Categories.Where(c => c.DeletedAt != null)
                    .ToListAsync();
                var categoriesSto = categories.Select(c => c.ToCategoryDto()).ToList();
                return new Result<List<CategoryDto>>(categoriesSto);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<CategoryDto>>(errorMessage);
            }
        }


    }
}
