using DP_Shop.Data.Entities;
using Microsoft.AspNetCore.Identity;
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
    }
}
