using DP_Shop.Data.DP_Shop.Data;
using DP_Shop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DP_Shop.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CategoryImage> CategoryImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Provinces> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd() 
                    .IsRequired(); 
            });

            builder.Entity<UserAddress>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(ua => ua.UserId);

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder.Entity<Provinces>()
                .HasMany(p => p.Districts)
                .WithOne(u => u.Provinces) 
                .HasForeignKey(d => d.ParentCode)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<District>()
                .HasMany(d => d.Wards)
                .WithOne(w => w.District)
                .HasForeignKey(w => w.ParentCode)
                .OnDelete(DeleteBehavior.Cascade);

            Seed.ProductImageCategorySeed(builder);
        }
    }
}
