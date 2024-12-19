using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.Products;

namespace DP_Shop.Mappers
{
    public static class ProductMappers
    {
        public static Product ToProduct(this ProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                Description = productDto.Description,   
                CreatedAt = productDto.CreatedAt,   
                UpdatedAt = productDto.UpdatedAt,
                DeletedAt = productDto.DeletedAt,
                ExpiryDate = productDto.ExpiryDate
            };
        }
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,    
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                DeletedAt = product.DeletedAt,
                ExpiryDate = product.ExpiryDate,
                CategoryId = product.CategoryId,    
            };
        }

        public static ProductDtoResponse ToProductDtoResponse(this Product product)
        {
            return new ProductDtoResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                DeletedAt = product.DeletedAt,
                ExpiryDate = product.ExpiryDate,
                CategoryId = product.CategoryId,
                Slug = product.Slug,    
            };
        }
        public static Product ToProduct(this CreateProductRequest productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                Description = productDto.Description,
                CreatedAt =DateTime.Now,
                ExpiryDate = productDto.ExpiryDate,
                CategoryId = productDto.CategoryId,
            };
        }

       

    }
}
