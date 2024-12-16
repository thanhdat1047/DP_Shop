using DP_Shop.Data.Entities;
using DP_Shop.DTOs.Categories;
using DP_Shop.DTOs.Images;

namespace DP_Shop.Mappers
{
    public static class ImageMapper
    {
        public static Image ToImage(this ImageDto request)
        {
            return new Image
            {
                Url = request.Url,
                Description = request.Description,
            };
        }

        public static ImageDto ToImageDto(this Image request)
        {
            return new ImageDto
            {
                Url = request.Url,
                Description = request.Description,
            };
        }
    }
}
