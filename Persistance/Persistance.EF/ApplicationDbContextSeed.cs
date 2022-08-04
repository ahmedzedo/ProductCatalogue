using Microsoft.AspNetCore.Identity;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Infrastructure.Identity;
using ProductCatalogue.Persistence.EF;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EF
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Products.Any())
            {
                context.Products.Add(new Product
                {
                    Name = "Product1",
                    Description = "this is the Product 1",
                    Picture = "1200px-Product_Photography.jpg",
                    Price = 100,
                });
                context.Products.Add(
                new Product
                {
                    Name = "Product2",
                    Description = "this is the Product 2",
                    Picture = "8801-Product-Images-3000x2000-Violet-FrontL30-1024x683.jpg",
                    Price = 90,
                });
                context.Products.Add(
                 new Product
                 {
                     Name = "Product3",
                     Description = "this is the Product 3",
                     Picture = "august-doorbell-cam-pro-product-photos-1.jpg",
                     Price = 110,
                 });
                context.Products.Add(
                 new Product
                 {
                     Name = "Product4",
                     Description = "this is the Product 4",
                     Picture = "card-smartphones.jpg",
                     Price = 120,
                 });
                context.Products.Add(
                 new Product
                 {
                     Name = "Product5",
                     Description = "this is the Product 5",
                     Picture = "dm430e-displays.jpg",
                     Price = 70,
                 });
                context.Products.Add(
                 new Product
                 {
                     Name = "Product6",
                     Description = "this is the Product 6",
                     Picture = "dmps-lite_product_thumb.jpg",
                     Price = 50,
                 });
                context.Products.Add(
                 new Product
                 {
                     Name = "Product7",
                     Description = "this is the Product 7",
                     Picture = "download (1).jfif",
                     Price = 180,
                 });
                context.Products.Add(
                 new Product
                 {
                     Name = "Product8",
                     Description = "this is the Product 8",
                     Picture = "dustin-brown-camera-vray-rhino-07.jpg",
                     Price = 200,
                 });
                context.Products.Add(
                 new Product
                 {
                     Name = "Product9",
                     Description = "this is the Product 9",
                     Picture = "images.jfif",
                     Price = 350,
                 });
                context.Products.Add(
                 new Product
                 {
                     Name = "Product10",
                     Description = "this is the Product 10",
                     Picture = "JBL_LIVE_660NC_Product image_Hero_White.jpg",
                     Price = 170,
                 });

                await context.SaveChangesAsync();
            }
        }
        public static void PrintData(ApplicationDbContext context)
        {
            // Gets and prints all books in database

            var products = context.Products;

            foreach (var product in products)
            {
                var data = new StringBuilder();
                data.AppendLine($"Name: {product.Name}");
                data.AppendLine($"Description: {product.Description}");
                data.AppendLine($"Price: {product.Price}");
                Console.WriteLine(data.ToString());
            }

        }
    }
}
