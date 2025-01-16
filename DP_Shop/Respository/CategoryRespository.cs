using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Images;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
            IDbContextTransaction? transaction = null;
            try
            {
                transaction = await _context.Database.BeginTransactionAsync();

                var newCategory = createCategory.ToCategory();
                await _context.Categories.AddAsync(newCategory);

                var image = createCategory.ImageDto.ToImage();
                await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();

                var categoryImage = new CategoryImage
                {
                    CategoryId = newCategory.Id,
                    ImageId = image.Id,
                };

                await _context.CategoryImages.AddAsync(categoryImage);
                await _context.SaveChangesAsync();

                // Commit
                await transaction.CommitAsync();
                return new Result<CategoryDto>(newCategory.ToCategoryDto());
            }
            catch (Exception ex) 
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<CategoryDto>(errorMessage);
            }
        }

        

        public async Task<Result<List<CategoryResponse>>> GetAll()
        {
            try
            {
                var categories = await _context.Categories
                    .AsNoTracking()
                    .Where(c => c.DeletedAt == null).ToListAsync();
                
                var listCategoryResponse = new List<CategoryResponse>();
                foreach (var category in categories) {
                    var imageDto = await _context.CategoryImages
                        .Where(ci => ci.CategoryId == category.Id)
                        .Select(ci => new ImageDto
                        {
                            Id = ci.Image!.Id,
                            Url = ci.Image.Url,
                            Description = ci.Image.Description
                        })
                        .FirstOrDefaultAsync();

                    var productCount = await _context.Products.AsNoTracking()
                        .Where(p => p.CategoryId == category.Id)
                        .CountAsync();

                    listCategoryResponse.Add(new CategoryResponse
                    {
                        CategoryDto = category.ToCategoryDto(),
                        ImageDto = imageDto,
                        ProductCount = productCount 
                    });
                }
                


                return new Result<List<CategoryResponse>>(listCategoryResponse);
            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<CategoryResponse>>(errorMessage);
            }
        }

        public async Task<Result<CategoryResponse>> GetById(int id)
        {
            try
            {
                var category = await _context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id); 
                if(category == null)
                {
                    return new Result<CategoryResponse>("Category not found");
                }

                var imageDto = await _context.CategoryImages
                       .Where(ci => ci.CategoryId == category.Id)
                       .Select(ci => new ImageDto
                       {
                           Id = ci.Image!.Id,
                           Url = ci.Image.Url,
                           Description = ci.Image.Description
                       })
                       .FirstOrDefaultAsync();
                var productCount = await _context.Products
                       .Where(p => p.CategoryId == category.Id)
                       .CountAsync();
                var response  = new CategoryResponse
                {
                    CategoryDto = category.ToCategoryDto(),
                    ImageDto = imageDto,
                    ProductCount = productCount
                };
                return new Result<CategoryResponse>(response);

            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<CategoryResponse>(errorMessage);
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
        public async Task<Result<List<CategoryResponse>>> GetSoftDeletedList()
        {
            try
            {
                var categories = await _context.Categories
                    .AsNoTracking().Where(c => c.DeletedAt != null)
                    .ToListAsync();

                var listCategoryResponse = new List<CategoryResponse>();
                foreach (var category in categories)
                {
                    var imageDto = await _context.CategoryImages
                        .Where(ci => ci.CategoryId == category.Id)
                        .Select(ci => new ImageDto
                        {
                            Id = ci.Image!.Id,
                            Url = ci.Image.Url,
                            Description = ci.Image.Description
                        })
                        .FirstOrDefaultAsync();
                    var productCount = await _context.Products
                        .AsNoTracking()
                        .Where(p => p.CategoryId == category.Id)
                        .CountAsync();

                    listCategoryResponse.Add(new CategoryResponse
                    {
                        CategoryDto = category.ToCategoryDto(),
                        ImageDto = imageDto,
                        ProductCount = productCount 
                    });
                }

                return new Result<List<CategoryResponse>>(listCategoryResponse);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<CategoryResponse>>(errorMessage);
            }
        }


    }
}
