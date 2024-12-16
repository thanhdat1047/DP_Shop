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
        public async Task<Result<ProductDto>> CreateAsync(ProductWithImagesRequest model)
        {
            IDbContextTransaction transaction = null;
            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();
                
                // Kiem tra ngay het han
                if (model.Product.ExpiryDate < DateTime.Now)
                {
                    return new Result<ProductDto>("The expiration date must be after the current date.");
                }

                // Kiem tra category hop le
                if(!await _dbContext.Categories.AnyAsync(c => c.Id == model.Product.CategoryId))
                {
                    return new Result<ProductDto>("Category not found");
                }

                // Tao product
                var newProduct = model.Product.ToProduct();
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

                return new Result<ProductDto>(newProduct.ToProductDto());   
            }
            catch (Exception ex)
            {
                if(transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ProductDto>(errorMessage);
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
            try
            {
                var products = _dbContext.Products
                    .AsQueryable()
                    .Where(p => p.DeletedAt == null);

                if (!string.IsNullOrEmpty(query.Name))
                {
                    products = products.Where(p => p.Name.Contains(query.Name));
                }

                if (!string.IsNullOrEmpty(query.SortBy))
                {
                    switch (query.SortBy.ToLower())
                    {
                        case "name":
                            products = query.isDecsending ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name);
                            break;
                        case "price":
                            products = query.isDecsending ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
                            break;
                        case "expirydate":
                            products = query.isDecsending ? products.OrderByDescending(p => p.ExpiryDate) : products.OrderBy(p => p.ExpiryDate);
                            break;
                        default:
                            products = products.OrderBy(p => p.Name);
                            break;
                    }
                }
                else
                {
                    products = products.OrderBy(p => p.Name);
                }

                var skip = (query.PageNumber - 1) * query.PageSize;
                var pageProducts = await products.Skip(skip).Take(query.PageSize).ToListAsync();

                var listProductResponse = new List<ProductResponse>();  

                foreach(var product in pageProducts)
                {
                    var imageDtos  = await _dbContext.ProductImages
                        .Where(pi => pi.ProductId == product.Id)
                        .Select(pi => pi.Image)
                        .Select(image => new ImageDto
                        {
                            Url = image.Url,
                            Description = image.Description
                        })
                        .ToListAsync();

                    listProductResponse.Add(new ProductResponse
                    {
                        Product = product.ToProductDto(),   
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

        public async Task<Result<ProductResponse>> GetById(int id)
        {
            try
            {
                if(!await ProductExists(id))
                {
                    return new Result<ProductResponse>("Product not found");
                }

                var product = await _dbContext.Products
                    .AsNoTracking()
                    .SingleOrDefaultAsync(p => p.Id == id);

                var images = await _dbContext.ProductImages
                    .AsNoTracking()
                    .Where(pi => pi.ProductId == id)
                    .Select(pi => pi.Image)
                    .Select(image => new ImageDto
                    {
                        Url = image.Url,
                        Description = image.Description
                    }).ToListAsync();

                var productResponse = new ProductResponse
                {
                    Product = product.ToProductDto(),
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

        public async Task<Result<List<ProductResponse>>> GetSoftDeletedList(QueryProducts query)
        {
            try
            {
                var products = _dbContext.Products
                    .AsQueryable()
                    .Where(p => p.DeletedAt != null);

                if (!string.IsNullOrEmpty(query.Name))
                {
                    products = products.Where(p => p.Name.Contains(query.Name));
                }

                if (!string.IsNullOrEmpty(query.SortBy))
                {
                    switch (query.SortBy.ToLower())
                    {
                        case "name":
                            products = query.isDecsending ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name);
                            break;
                        case "price":
                            products = query.isDecsending ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
                            break;
                        case "expirydate":
                            products = query.isDecsending ? products.OrderByDescending(p => p.ExpiryDate) : products.OrderBy(p => p.ExpiryDate);
                            break;
                        default:
                            products = products.OrderBy(p => p.Name);
                            break;
                    }
                }
                else
                {
                    products = products.OrderBy(p => p.Name);
                }

                var skip = (query.PageNumber - 1) * query.PageSize;
                var pageProducts = await products.Skip(skip).Take(query.PageSize).ToListAsync();

                var listProductResponse = new List<ProductResponse>();

                foreach (var product in pageProducts)
                {
                    var imageDtos = await _dbContext.ProductImages
                        .Where(pi => pi.ProductId == product.Id)
                        .Select(pi => pi.Image)
                        .Select(image => new ImageDto
                        {
                            Url = image.Url,
                            Description = image.Description
                        })
                        .ToListAsync();

                    listProductResponse.Add(new ProductResponse
                    {
                        Product = product.ToProductDto(),
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

        public async Task<Result<ProductDto>> UpdateAsync(int id, CreateProductRequest model)
        {
            IDbContextTransaction transaction = null;
            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();

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

               /* var newImages = model.ImageDtos.Select(img => new Image
                {
                    Url = img.Url,
                    Description = img.Description,
                }).ToList();

                // Xoa anh cu neu co
                var imagesToRemove = existingProduct.ProductImages
                    .Select(pi => pi.Image)
                    .Where(image => image != null) 
                    .ToList();

                if (imagesToRemove.Any())  
                {
                    _dbContext.Images.RemoveRange(imagesToRemove!);
                }
                // Them hinh anh moi
                await _dbContext.Images.AddRangeAsync(newImages);
                await _dbContext.SaveChangesAsync();


                // Cap nhat lien ket hinh anh va product
                var updatedProductImages = newImages.Select(image => new ProductImage
                {
                    ProductId = existingProduct.Id,
                    ImageId = image.Id,
                }).ToList();

                // Xoa lien ket cu
                _dbContext.ProductImages.RemoveRange(existingProduct.ProductImages);

                // Them lien ket moi
                await _dbContext.ProductImages.AddRangeAsync(updatedProductImages);
                await _dbContext.SaveChangesAsync();*/

                // Luu 
                await _dbContext.SaveChangesAsync();

                // Commit
                await transaction.CommitAsync();

               /* var listImageDtos = new List<ImageDto>();
                foreach(var img in newImages)
                {
                    listImageDtos.Add(img.ToImageDto());    
                }*/

                //var productResponse = existingProduct.ToProductDto();

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
    }
}
