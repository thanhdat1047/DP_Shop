using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Images;
using DP_Shop.DTOs.Products;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Mappers;
using DP_Shop.Models.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DP_Shop.Respository
{
    public class ProductRespository : IProductRespository
    {
        private readonly AppDbContext _dbContext;
        public ProductRespository(AppDbContext dbContext)
        {
            _dbContext = dbContext; 

        }
        // them san pham
        public async Task<Result<ProductDtoResponse>> CreateAsync(ProductWithImagesRequest model)
        {
            IDbContextTransaction transaction = null;
            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();
                
                // Kiem tra ngay het han
                if (model.Product.ExpiryDate < DateTime.Now)
                {
                    return new Result<ProductDtoResponse>("The expiration date must be after the current date.");
                }

                // Kiem tra category hop le
                if(!await _dbContext.Categories.AnyAsync(c => c.Id == model.Product.CategoryId))
                {
                    return new Result<ProductDtoResponse>("Category not found");
                }

                
                // Tao product
                var newProduct = model.Product.ToProduct();

                newProduct.GenerateSlug();
                var exestingProduct = await _dbContext.Products
                    .FirstOrDefaultAsync(p => p.Slug == newProduct.Slug);
                if (exestingProduct != null) 
                {
                    return new Result<ProductDtoResponse>("A product with this slug already exists.");
                }


                await _dbContext.Products.AddAsync(newProduct);

                var images = model.ImageDtos.Select( img => new Image
                {
                    Url = img.Url,  
                    Description = img.Description,
                }).ToList();

                // Tao hinh anh
                await _dbContext.Images.AddRangeAsync(images);
                await _dbContext.SaveChangesAsync();

                // Tao lien ket hinh anh
                var productImages = images.Select(image => new ProductImage
                {
                    ProductId = newProduct.Id,
                    ImageId = image.Id,
                }).ToList();
                await _dbContext.ProductImages.AddRangeAsync(productImages);

                // Luu 
                await _dbContext.SaveChangesAsync();
                
                // Commit
                await transaction.CommitAsync();

                return new Result<ProductDtoResponse>(newProduct.ToProductDtoResponse());   
            }
            catch (Exception ex)
            {
                if(transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ProductDtoResponse>(errorMessage);
            }
        }

        public async Task<Result<bool>> DeleteById(int id)
        {
            try
            {
                var product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
                if(product == null)
                {
                    return new Result<bool>("Product not found");
                }

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return new Result<bool>(true);
            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }

        public async Task<Result<List<ProductResponse>>> GetAll(QueryProducts query)
        {
            return await GetProductsWithFilters(query, productsQuery => productsQuery.Where(p => p.DeletedAt == null));
        }
        public async Task<Result<List<ProductResponse>>> GetProductByCategoryId(QueryProducts query, int cateId)
        {
            return await GetProductsWithFilters(query, productsQuery => productsQuery.Where(p => p.DeletedAt == null && p.CategoryId == cateId));  
        }
        public async Task<Result<List<ProductResponse>>> GetSoftDeletedList(QueryProducts query)
        {
            return await GetProductsWithFilters(query, productsQuery => productsQuery.Where(p => p.DeletedAt != null));
        }

        public async Task<Result<ProductResponse>> GetBySlug(string slug)
        {
            try
            {

                var product = await _dbContext.Products
                    .AsNoTracking()
                    .SingleOrDefaultAsync(p => p.Slug == slug);

                if (product == null)
                {
                    return new Result<ProductResponse>("Product not found");
                }
                var images = await _dbContext.ProductImages
                    .AsNoTracking()
                    .Where(pi => pi.ProductId == product.Id)
                    .Select(pi => pi.Image)
                    .Select(image => new ImageDto
                    {
                        Id = image!.Id,
                        Url = image!.Url,
                        Description = image.Description
                    }).ToListAsync();

                var productResponse = new ProductResponse
                {
                    Product = product.ToProductDtoResponse(),
                    ImageDtos = images
                };

                return new Result<ProductResponse>(productResponse);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ProductResponse>(errorMessage);
            }
        }
        public async Task<Result<ProductResponse>> GetById(int id)
        {
            try
            {

                var product = await _dbContext.Products
                    .AsNoTracking()
                    .SingleOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    return new Result<ProductResponse>("Product not found");
                }
                var images = await _dbContext.ProductImages
                    .AsNoTracking()
                    .Where(pi => pi.ProductId == id)
                    .Select(pi => pi.Image)
                    .Select(image => new ImageDto
                    {
                        Id = image!.Id,
                        Url = image!.Url,
                        Description = image.Description
                    }).ToListAsync();

                var productResponse = new ProductResponse
                {
                    Product = product.ToProductDtoResponse(),
                    ImageDtos = images
                };

                return new Result<ProductResponse>(productResponse);
            }
            catch (Exception ex) 
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ProductResponse>(errorMessage);
            }

        }

        public async Task<bool> ProductExists(int id)
        {
            return await _dbContext.Products.AnyAsync();
        }

        public async Task<Result<bool>> RestoreAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);
                if (product == null)
                {
                    return new Result<bool>("Product not found");
                }
                product.DeletedAt = null;
                await _dbContext.SaveChangesAsync();

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
                var product = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);
                if (product == null)
                {
                    return new Result<bool>("Product not found");
                }
                product.DeletedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();

                return new Result<bool>(true);

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }

        public async Task<Result<ProductDto>> UpdateAsync(int id, UpdateProductRequest model)
        {
            IDbContextTransaction transaction = null;
            try
            {
                
                if(model.Id != id)
                {
                    return new Result<ProductDto>("Id is not matched");
                }
                // Kiem tra ngay het han
                if (model.ExpiryDate < DateTime.Now)
                {
                    return new Result<ProductDto>("The expiration date must be after the current date.");
                }

                // Kiem tra category hop le
                if (!await _dbContext.Categories.AnyAsync(c => c.Id == model.CategoryId))
                {
                    return new Result<ProductDto>("Category not found");
                }
                transaction = await _dbContext.Database.BeginTransactionAsync();
                // Lay san pham hien tai
                var existingProduct = await _dbContext.Products
                    .Include(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingProduct == null)
                {
                    return new Result<ProductDto>("Product not found");
                }

                // Cap nhat du lieu

                existingProduct.Name = model.Name;
                existingProduct.Description = model.Description;
                existingProduct.Price = model.Price;
                existingProduct.ExpiryDate = model.ExpiryDate;
                existingProduct.CategoryId = model.CategoryId;
                existingProduct.UpdatedAt = DateTime.Now;

                if(existingProduct.Name != model.Name)
                {
                    existingProduct.GenerateSlug();

                    var duplicateSlug = await _dbContext.Products
                        .FirstOrDefaultAsync(p => p.Slug == existingProduct.Slug && p.Id != id);
                    if(duplicateSlug != null)
                    {
                        return new Result<ProductDto>("The generated slug already exists. Please use a different name. ");
                    }
                }

                _dbContext.Products.Update(existingProduct);    
                // Luu 
                await _dbContext.SaveChangesAsync();

                // Commit
                await transaction.CommitAsync();


                return new Result<ProductDto>(existingProduct.ToProductDto());
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ProductDto>(errorMessage);
            }
        }


        public async Task<Result<List<ProductResponse>>> GetProductsWithFilters(QueryProducts query, Func<IQueryable<Product>, IQueryable<Product>>? filter = null)
        {
            try 
            {
                var productsQuery = _dbContext.Products.AsQueryable();
                if(filter != null)
                {
                    productsQuery = filter(productsQuery);    
                }
                if(!string.IsNullOrEmpty(query.Name))
                {
                    productsQuery = productsQuery.Where(p => p.Name.Contains(query.Name));    
                }

                if (!string.IsNullOrEmpty(query.SortBy))
                {
                    productsQuery = ApplySorting(productsQuery, query);
                }
                else
                {
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                }
                // Phân trang
                var skip = (query.PageNumber - 1) * query.PageSize;
                var pageProducts = await productsQuery.Skip(skip).Take(query.PageSize).ToListAsync();


                var listProductResponse = new List<ProductResponse>();
                foreach (var product in pageProducts)
                {
                    // Lấy thông tin hình ảnh cho từng sản phẩm
                    var imageDtos = await _dbContext.ProductImages
                        .Where(pi => pi.ProductId == product.Id)
                        .Select(pi => new ImageDto
                        {
                            Id = pi.Image!.Id,
                            Url = pi.Image.Url,
                            Description = pi.Image.Description
                        })
                        .ToListAsync();

                    listProductResponse.Add(new ProductResponse
                    {
                        Product = product.ToProductDtoResponse(),
                        ImageDtos = imageDtos
                    });
                }

                return new Result<List<ProductResponse>>(listProductResponse);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<ProductResponse>>(errorMessage);
            }
        }
        private IQueryable<Product> ApplySorting(IQueryable<Product> productsQuery, QueryProducts query)
        {
            switch (query.SortBy!.ToLower())
            {
                case "name":
                    return query.isDecsending ? productsQuery.OrderByDescending(p => p.Name) : productsQuery.OrderBy(p => p.Name);
                case "price":
                    return query.isDecsending ? productsQuery.OrderByDescending(p => p.Price) : productsQuery.OrderBy(p => p.Price);
                case "expirydate":
                    return query.isDecsending ? productsQuery.OrderByDescending(p => p.ExpiryDate) : productsQuery.OrderBy(p => p.ExpiryDate);
                default:
                    return productsQuery.OrderBy(p => p.Name);
            }
        }


    }
}
