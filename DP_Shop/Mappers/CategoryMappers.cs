using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Categories;

namespace DP_Shop.Mappers
{
    public static class CategoryMappers
    {
        public static Category ToCategory(this CreateCategoryRequest request)
        {
            return new Category
            {
                Name = request.Name,    
                Description = request.Description,
            };
        }

        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,   
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}
