using DP_Shop.Data.Entities;
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
                Image = productDto.Image,
                Description = productDto.Description,   
                CreatedAt = productDto.CreatedAt,   
                UpdatedAt = productDto.UpdatedAt,
                DeletedAt = productDto.DeletedAt,
                ExpiryDate = productDto.ExpiryDate, 
            };
        }
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                DeletedAt = product.DeletedAt,
                ExpiryDate = product.ExpiryDate,
            };
        }
        public static Product ToProduct(this CreateProductRequest productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                Image = productDto.Image,
                Description = productDto.Description,
                CreatedAt =DateTime.Now,
                ExpiryDate = productDto.ExpiryDate,
                CategoryId = productDto.CategoryId,
            };
        }


    }
}
