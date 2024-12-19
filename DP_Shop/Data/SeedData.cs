using DP_Shop.Data.Entities;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DP_Shop.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new[] { "User", "Admin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@dp-shop.com");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@dp-shop.com",
                    Email = "admin@dp-shop.com",
                };

                // mat khau
                var createAdminResult = await userManager.CreateAsync(adminUser, "Admin@1234"); 

                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Failed to create admin account.");
                }
            }

            var user = await userManager.FindByEmailAsync("user@dp-shop.com");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "user@dp-shop.com",
                    Email = "user@dp-shop.com",
                };

                var createUserResult = await userManager.CreateAsync(user, "User@1234"); // Mật khẩu user

                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    throw new Exception("Failed to create user.");
                }
            }
        }
        
        public static void ProductImageCategorySeed(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Sản phẩm truyền thống",
                    Description = "Sản phẩm truyền thống",
                    CreatedAt = DateTime.Now

                },
                new Category
                {
                    Id = 2,
                    Name = "Sản phẩm hiện đại",
                    Description = "Sản phẩm hiện đại",
                    CreatedAt = DateTime.Now

                }
            );

            // Seek Images 
            modelBuilder.Entity<Image>().HasData(
                new Image
                {
                    Id = 1,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734498100/chinh-300x300_tfdduo.png",
                    Description = "Bánh Ép Huế Vị Thịt Nướng 40g"
                }
                , 
                new Image
                {
                    Id = 2,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734498099/Artboard-391-300x300_o3uacb.png",
                    Description = "Bánh Ép Huế Vị Thịt Nướng 40g"
                }, 
                new Image
                {
                    Id = 3,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579317/2-300x300_rpepfq.jpg",
                    Description = "Bánh Ép Huế Gói Snack Vị Tép 40g"
                }, 
                new Image
                {
                    Id = 4,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579317/3_ciytdq.jpg",
                    Description = "Bánh Ép Huế Gói Snack Vị Tép 40g"
                }, 
                new Image
                {
                    Id = 5,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579318/Thiet-ke-chua-co-ten_w83xbi.jpg",
                    Description = "Bánh Ép Huế Gói Snack Vị Tép 40g"
                }, 
                new Image
                {
                    Id = 6,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579544/5-300x300_xkcm3z.jpg",
                    Description = "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g"
                }, 
                new Image
                {
                    Id = 7,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579544/4-300x300_kkdq5f.jpg",
                    Description = "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g"
                }, 
                new Image
                {
                    Id = 8,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579544/1-300x300_yn7thr.jpg",
                    Description = "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g"
                }, 
                new Image
                {
                    Id = 9,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579658/Artboard-351-300x300_ceyyrk.png",
                    Description = "Bánh Ép Huế Hải Sản Vị Tôm Thịt 65G"
                },
                new Image
                {
                    Id = 10,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579658/chinh-2-300x300_cf7cqt.png",
                    Description = "Bánh Ép Huế Hải Sản Vị Tôm Thịt 65G"
                },
                new Image
                {
                    Id = 11,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579954/Artboard-361-300x300_ie5h9h.png",
                    Description = "Bánh Ép Huế Hải Sản Vị Tôm Thịt 40G"
                },
                new Image
                {
                    Id = 12,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579954/chinh-3-300x300_j5x0wl.png",
                    Description = "Bánh Ép Huế Hải Sản Vị Tôm Thịt 40G"
                },
                new Image
                {
                    Id = 13,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580057/Sen-768x576_mth6jv.png",
                    Description = "Bánh ép Huế chay vị Sen Huế 65G"
                },
                new Image
                {
                    Id = 14,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580131/9-300x300_rzn0md.jpg",
                    Description = "Bánh ép Huế chay vị Nấm Hương 65G"
                },
                new Image
                {
                    Id = 15,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580132/12-300x300_mljbgd.jpg",
                    Description = "Bánh ép Huế chay vị Nấm Hương 65G"
                },
                new Image
                {
                    Id = 16,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580133/Nam-1-300x300_vfz61t.png",
                    Description = "Bánh ép Huế chay vị Nấm Hương 65G"
                },
                new Image
                {
                    Id = 17,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580226/Artboard-391-300x300_jvirsd.png",
                    Description = "Bánh Ép Huế BBQ Vị Thịt Nướng 65G"
                },
                new Image
                {
                    Id = 18,
                    Url = "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580226/chinh-300x300_yfjqj7.png",
                    Description = "Bánh Ép Huế BBQ Vị Thịt Nướng 65G"
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Bánh Ép Huế Vị Thịt Nướng 40g",
                    Price = 30000,
                    Description = "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.",
                    Quantity = 50,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    CategoryId = 1,
                    Slug = "banh-ep-hue-vi-thi-nuong-40g"
                },
                new Product
                {
                    Id = 2,
                    Name = "Bánh Ép Huế Gói Snack Vị Tép 40g",
                    Price = 35000,
                    Description = "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.",
                    Quantity = 50,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    CategoryId = 2,
                    Slug = "banh-ep-hue-goi-snack-vi-tep-40g"
                },
                new Product
                {
                    Id = 3,
                    Name = "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g",
                    Price = 35000,
                    Description = "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.",
                    Quantity = 50,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    CategoryId = 2,
                    Slug = "banh-ep-hue-goi-snack-bbq-vi-thit-nuong-40g"
                },
                new Product
                {
                    Id = 4,
                    Name = "Bánh Ép Huế Hải Sản Vị Tôm Thịt 65G",
                    Price = 50000,
                    Description = "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.",
                    Quantity = 50,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    CategoryId = 1,
                    Slug = "banh-ep-hue-hai-san-vi-tom-thit-65g"
                },
                new Product
                {
                    Id = 5,
                    Name = "Bánh Ép Huế Hải Sản Vị Tôm Thịt 40G",
                    Price = 30000,
                    Description = "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.",
                    Quantity = 50,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    CategoryId = 1,
                    Slug = "banh-ep-hue-hai-san-vi-tom-thit-40g"
                },
                new Product
                {
                    Id = 6,
                    Name = "Bánh ép Huế chay vị Sen Huế 65G",
                    Price = 50000,
                    Description = "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.",
                    Quantity = 50,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    CategoryId = 1,
                    Slug = "banh-ep-hue-chay-vi-sen-hue-65g"
                },
                new Product
                {
                    Id = 7,
                    Name = "Bánh ép Huế chay vị Nấm Hương 65G",
                    Price = 50000,
                    Description = "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.",
                    Quantity = 20,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    CategoryId = 1,
                    Slug = "banh-ep-hue-chay-vi-nam-huong-65g"
                },
                new Product
                {
                    Id = 8,
                    Name = "Bánh Ép Huế BBQ Vị Thịt Nướng 65G",
                    Price = 50000,
                    Description = "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.",
                    Quantity = 20,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                    CategoryId = 1,
                    Slug = "banh-ep-hue-bbq-vi-thit-nuong-65g"
                }
            );

            // Seed ProductImages
            modelBuilder.Entity<ProductImage>().HasData
            (
                new ProductImage
                {
                    Id = 1,
                    ProductId = 1,
                    ImageId = 1
                },
                new ProductImage
                {
                    Id = 2,
                    ProductId = 1,
                    ImageId = 2
                },
                new ProductImage
                {
                    Id = 3,
                    ProductId = 2,
                    ImageId = 3
                },
                new ProductImage
                {
                    Id = 4,
                    ProductId = 2,
                    ImageId = 4
                }, 
                new ProductImage
                {
                    Id = 5,
                    ProductId = 2,
                    ImageId = 5
                }, 
                new ProductImage
                {
                    Id = 6,
                    ProductId = 3,
                    ImageId = 6
                }, 
                new ProductImage
                {
                    Id = 7,
                    ProductId = 3,
                    ImageId = 7
                }, 
                new ProductImage
                {
                    Id = 8,
                    ProductId = 3,
                    ImageId = 8
                },
                new ProductImage
                {
                    Id = 9,
                    ProductId = 4,
                    ImageId = 9
                },
                new ProductImage
                {
                    Id = 10,
                    ProductId = 4,
                    ImageId = 10
                },
                new ProductImage
                {
                    Id = 11,
                    ProductId = 5,
                    ImageId = 11
                },
                new ProductImage
                {
                    Id = 12,
                    ProductId = 5,
                    ImageId = 12
                },
                new ProductImage
                {
                    Id = 13,
                    ProductId = 6,
                    ImageId = 13
                },
                new ProductImage
                {
                    Id = 14,
                    ProductId = 7,
                    ImageId = 14
                },
                new ProductImage
                {
                    Id = 15,
                    ProductId = 7,
                    ImageId = 15
                },
                new ProductImage
                {
                    Id = 16,
                    ProductId = 7,
                    ImageId = 16
                },
                new ProductImage
                {
                    Id = 17,
                    ProductId = 8,
                    ImageId = 17
                },
                new ProductImage
                {
                    Id = 18,
                    ProductId = 8,
                    ImageId = 18
                }
            );
        }
    }
}
