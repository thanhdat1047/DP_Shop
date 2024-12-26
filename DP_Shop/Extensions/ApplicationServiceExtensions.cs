using DP_Shop.Data;
using DP_Shop.Interface;
using DP_Shop.Respository;
using DP_Shop.Services;
using Microsoft.EntityFrameworkCore;

namespace DP_Shop.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountRespository, AccountRespository>();
            services.AddScoped<IUserRespository, UserRespository>();
            services.AddScoped<IRoleRespository, RoleRespository>();
            services.AddScoped<IAddressRepository, AddressRespository>();
            services.AddScoped<ICategoryRespository, CategoryRespository>();
            services.AddScoped<IProductRespository, ProductRespository>();
            services.AddScoped<IImageRespository, ImageRespository>();  
            services.AddScoped<ICartRespository, CartRespository>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
