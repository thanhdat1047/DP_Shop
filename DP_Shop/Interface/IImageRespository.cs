using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Images;
using DP_Shop.Helpers.Query;
using DP_Shop.Models.Result;

namespace DP_Shop.Interface
{
    public interface IImageRespository
    {
        Task<Result<ImageDto>> GetImageById (int id);   
        Task<Result<List<ImageDto>>> GetImages(QueryImages query);
        Task<Result<List<ImageDto>>> GetImagesByProductId(int productId);
        Task<Result<ImageDto>> AddImage(ImageRequest imageRequest, int productId);
        Task<Result<List<ImageDto>>> UpdateImagesOfProduct(ImageDto image); 
        Task<Result<Boolean>> DeleteImageOfProduct(ImageDto image, int productId);

    }
}
