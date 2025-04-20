using Microsoft.EntityFrameworkCore;

namespace BinhDinhFood.Infrastructure;

public class BinhDinhFoodDbContextInitializer(BinhDinhFoodDbContext context, ILoggerFactory logger)
{
    private readonly BinhDinhFoodDbContext _context = context;
    private readonly ILogger _logger = logger.CreateLogger<BinhDinhFoodDbContextInitializer>();

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError("Migration error {exception}", exception);
            throw;
        }
    }

    //private void CreateDatabaseScript()
    //{

    //    // Extract existing Products
    //    var existingProducts = _context.Products.ToList();
    //    foreach (var product in existingProducts)
    //    {
    //        Console.WriteLine($@"
    //         modelBuilder.Entity<Product>().HasData(new Product 
    //         {{
    //             ProductId = {product.ProductId},
    //             ProductName = ""{product.ProductName}"",
    //             ProductPrice = {product.ProductPrice},
    //             ProductDescription = ""{product.ProductDescription}"",
    //             ProductAmount = {product.ProductAmount},
    //             ProductDiscount = {product.ProductDiscount},
    //             ProductRating = {product.ProductRating},
    //             ProductImage = ""{product.ProductImage}"",
    //             ProductDateCreated = DateTime.Parse(""{product.ProductDateCreated:yyyy-MM-dd}""),
    //             CategoryId = {product.CategoryId}
    //         }});");
    //    }

    //    // Extract existing Categories
    //    var existingCategories = _context.Categories.ToList();
    //    foreach (var category in existingCategories)
    //    {
    //        Console.WriteLine($@"
    //         modelBuilder.Entity<Category>().HasData(new Category 
    //         {{
    //             CategoryId = {category.CategoryId},
    //             CategoryName = ""{category.CategoryName}"",
    //             CategoryDateCreated = DateTime.Parse(""{category.CategoryDateCreated:yyyy-MM-dd}"")
    //         }});");
    //    }

    //    // Extract existing Customers
    //    var existingCustomers = _context.Customers.ToList();
    //    foreach (var customer in existingCustomers)
    //    {
    //        Console.WriteLine($@"
    //         modelBuilder.Entity<Customer>().HasData(new Customer 
    //         {{
    //             CustomerId = {customer.CustomerId},
    //             CustomerFullName = ""{customer.CustomerFullName}"",
    //             CustomerUserName = ""{customer.CustomerUserName}"",
    //             CustomerPassword = ""{customer.CustomerPassword}"",
    //             CustomerDateCreated = DateTime.Parse(""{customer.CustomerDateCreated:yyyy-MM-dd}""),
    //             CustomerEmail = ""{customer.CustomerEmail}"",
    //             CustomerAddress = ""{customer.CustomerAddress}"",
    //             CustomerPhone = ""{customer.CustomerPhone}"",
    //             CustomerState = {customer.CustomerState.ToString().ToLower()},
    //             CustomerImage = ""{customer.CustomerImage}""
    //         }});");
    //    }

    //    // Extract existing Blogs
    //    var existingBlogs = _context.Blog.ToList();
    //    foreach (var blog in existingBlogs)
    //    {
    //        Console.WriteLine($@"
    //         modelBuilder.Entity<Blog>().HasData(new Blog 
    //         {{
    //             BlogId = {blog.BlogId},
    //             BlogName = ""{blog.BlogName}"",
    //             BlogContent = ""{blog.BlogContent}"",
    //             BlogImage = ""{blog.BlogImage}"",
    //             BlogDateCreated = DateTime.Parse(""{blog.BlogDateCreated:yyyy-MM-dd}"")
    //         }});");
    //    }

    //    //Extract existing Banners
    //    var existingBanners = _context.Banners.ToList();
    //    foreach (var banner in existingBanners)
    //    {
    //        Console.WriteLine($@"
    //        modelBuilder.Entity<Banner>().HasData(new Banner 
    //        {{
    //            BannerId = {banner.BannerId},
    //            BannerName = ""{banner.BannerName}"",
    //            ProductDiscount = {banner.ProductDiscount},
    //            BannerPrice = {banner.BannerPrice},
    //            BannerDescription = ""{banner.BannerDescription}"",
    //            BannerImage = ""{banner.BannerImage}"",
    //            BannerDateCreated = DateTime.Parse(""{banner.BannerDateCreated:yyyy-MM-dd}"")
    //        }});");
    //    }
    //    // Extract existing Admins
    //    var existingAdmins = _context.Admins.ToList();
    //    foreach (var admin in existingAdmins)
    //    {
    //        Console.WriteLine($@"
    //         modelBuilder.Entity<Admin>().HasData(new Admin 
    //         {{
    //             AdminId = {admin.AdminId},
    //             AdminUserName = ""{admin.AdminUserName}"",
    //             AdminPassword = ""{admin.AdminPassword}"",
    //             AdminEmail = ""{admin.AdminEmail}"",
    //             AdminImage = ""{admin.AdminImage}"",
    //             AdminDateCreated = DateTime.Parse(""{admin.AdminDateCreated:yyyy-MM-dd}"")
    //         }});");
    //    }
    //    return _context.Database.ExecuteSqlRawAsync(sqlCommand);
    //}
}
