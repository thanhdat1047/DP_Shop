using DP_Shop.Data;
using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Address;
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
    public class ImageRespository : IImageRespository
    {
        private readonly AppDbContext _dbContext;
        public ImageRespository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<ImageDto>> AddImage(ImageRequest imageRequest, int productId)
        {

            IDbContextTransaction? transaction = null;
            try
            {
                var product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == productId);
                if (product == null) 
                {
                    return new Result<ImageDto>("Product not found");
                }

                transaction = await _dbContext.Database.BeginTransactionAsync();

                var image = imageRequest.ToImage();
                await _dbContext.Images.AddAsync(image);
                await _dbContext.SaveChangesAsync();

                var productImage = new ProductImage
                {
                    ProductId = productId,  
                    ImageId = image.Id, 
                };
                await _dbContext.ProductImages.AddAsync(productImage);  

                await _dbContext.SaveChangesAsync();
                transaction.Commit();
                
                return new Result<ImageDto>(image.ToImageDto());    
            }
            catch (Exception ex) 
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ImageDto>(errorMessage);
            }
        }

        public async Task<Result<bool>> DeleteImageOfProduct(ImageDto image, int productId)
        {
            IDbContextTransaction? transaction = null;
            try
            {
                var productExists = await _dbContext.Products.AnyAsync(p => p.Id == productId);
                if (!productExists)
                {
                    return new Result<bool>("Product not found");
                }

                transaction = await _dbContext.Database.BeginTransactionAsync();

                var productImage = await _dbContext.ProductImages
                    .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.ImageId == image.Id);
                
                if(productImage != null)
                {
                    _dbContext.ProductImages.Remove(productImage);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return new Result<bool>("Image not associated with the product");
                }

                // kiem tra con lien ket voi sp khac
                var isImageUsedByOtherProducts = await _dbContext.ProductImages
                    .AnyAsync(pi => pi.ImageId == image.Id);
                // xoa, neu k
                if (!isImageUsedByOtherProducts) 
                {
                    var imageToDelete = await _dbContext.Images
                        .FirstOrDefaultAsync(i => i.Id == image.Id);
                    if(imageToDelete != null)
                    {
                        _dbContext.Images.Remove(imageToDelete);
                    }
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<bool>(errorMessage);
            }
        }

        public async Task<Result<ImageDto>> GetImageById(int id)
        {
            try
            {
                var image = await _dbContext.Images
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (image == null)
                {
                    return new Result<ImageDto>("Image not found");
                }
                
                return new Result<ImageDto>(image.ToImageDto());

            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<ImageDto>(errorMessage);
            }
        }

        public async Task<Result<List<ImageDto>>> GetImages(QueryImages query)
        {
            try
            {
                var images = _dbContext.Images.AsNoTracking().AsQueryable();
                images.OrderBy(i => i.Url); 

                var skip = (query.PageNumber - 1) * query.PageSize;
                var pageImages = await images.Skip(skip).Take(query.PageSize).ToListAsync();
                var pageImagesDto = new List<ImageDto>();   

                foreach(var i in pageImages) 
                {
                    pageImagesDto.Add(i.ToImageDto());
                }

                return new Result<List<ImageDto>>(pageImagesDto);

            }
            catch(Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<ImageDto>>(errorMessage);
            }
        }

        public async Task<Result<List<ImageDto>>> GetImagesByProductId(int productId)
        {
            try
            {
                var productExists = await _dbContext.Products
                    .AsNoTracking()
                    .AnyAsync(p => p.Id == productId);
                if(!productExists)
                {
                    return new Result<List< ImageDto >> ("Product not found");
                }
                var listImageDto = await _dbContext.ProductImages
                    .AsNoTracking()
                    .Where(pi => pi.ProductId == productId && pi.Image != null)
                    .Select(pi => new ImageDto
                    {
                        Id = pi.Image!.Id,
                        Url = pi.Image.Url,
                        Description = pi.Image.Description,

                    })
                    .ToListAsync();  

                return new Result<List<ImageDto>> (listImageDto);    
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}, StackTrace: {ex.StackTrace}";
                return new Result<List<ImageDto>>(errorMessage);
            }
           
        }

        public Task<Result<List<ImageDto>>> UpdateImagesOfProduct(ImageDto image)
        {
            throw new NotImplementedException();
        }
    }
}
