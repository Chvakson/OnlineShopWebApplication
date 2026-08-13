using GameOnlineStore.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShopWebApplication;

namespace GameOnlineStore.Db
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();
            await InitializeRolesAsync(roleManager); 
            await InitializeUsersAsync(userManager, roleManager);
            await InitializeProductsAsync(context);
        }

        private static async Task InitializeRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Создаем все необходимые роли
            var roles = new[] { Constants.AdminRoleName, Constants.UserRoleName, Constants.ModeratorRoleName };

            foreach (var roleName in roles)
            {
                await CreateRoleIfNotExistsAsync(roleManager, roleName);
            }
        }

        private static async Task InitializeUsersAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@gmail.com";
            var password = "_Aa123456";

            if (userManager.FindByNameAsync(adminEmail).Result == null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail };
                var result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Constants.AdminRoleName);
                }
            }
        }

        private static async Task CreateRoleIfNotExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                Console.WriteLine($"Роль '{roleName}' создана");
            }
        }

        private static async Task EnsureProductCatalogColumnsAsync(ApplicationContext context)
        {
            await context.Database.ExecuteSqlRawAsync(@"ALTER TABLE ""Products"" ADD COLUMN IF NOT EXISTS ""Genre"" text;");
            await context.Database.ExecuteSqlRawAsync(@"ALTER TABLE ""Products"" ADD COLUMN IF NOT EXISTS ""Developer"" text;");
            await context.Database.ExecuteSqlRawAsync(@"ALTER TABLE ""Products"" ADD COLUMN IF NOT EXISTS ""ReleaseYear"" integer;");
        }

        private static async Task InitializeProductsAsync(ApplicationContext context)
        {
            await EnsureProductCatalogColumnsAsync(context);

            var products = ProductCatalogSeed.All();

            foreach (var product in products)
            {
                var existing = await context.Products.FirstOrDefaultAsync(entity => entity.Name == product.Name);
                if (existing == null)
                {
                    await context.Products.AddAsync(product);
                    continue;
                }

                existing.Genre = product.Genre;
                existing.Developer = product.Developer;
                existing.ReleaseYear = product.ReleaseYear;
                existing.ImgFileName = product.ImgFileName;
                if (string.IsNullOrWhiteSpace(existing.Description))
                {
                    existing.Description = product.Description;
                }
            }

            await context.SaveChangesAsync();
        }
    }
}