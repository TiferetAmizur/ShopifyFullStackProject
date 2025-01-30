using Microsoft.EntityFrameworkCore;
using WebAppShopify.Models;

namespace WebAppShopify.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed users (admin and viewer)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("AdminPassword123"),
                    Role = "Admin"
                },
                new User
                {
                    UserId = 2,
                    Username = "viewer",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("ViewerPassword123"),
                    Role = "Viewer"
                }
            );

            // Seed products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = true, StockArrivalDate = DateTime.Parse("2025-02-01") },
                new Product { ProductId = 2, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = false, StockArrivalDate = DateTime.Parse("2025-03-01") },
                new Product { ProductId = 3, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = true, StockArrivalDate = null },
                new Product { ProductId = 4, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = true, StockArrivalDate = null },
                new Product { ProductId = 5, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = false, StockArrivalDate = DateTime.Parse("2025-02-15") },
                new Product { ProductId = 6, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = true, StockArrivalDate = DateTime.Parse("2025-01-30") },
                new Product { ProductId = 7, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = true, StockArrivalDate = null },
                new Product { ProductId = 8, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = false, StockArrivalDate = DateTime.Parse("2025-02-20") },
                new Product { ProductId = 9, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = true, StockArrivalDate = null },
                new Product { ProductId = 10, AddedTimestamp = DateTime.Parse("2025-01-28 17:04:07.0766667"), InStock = false, StockArrivalDate = DateTime.Parse("2025-03-10") }
            );

            // Seed product descriptions
            modelBuilder.Entity<ProductDescription>().HasData(
                new ProductDescription { ProductId = 1, ProductName = "Samsung Galaxy S23 Ultra" },
                new ProductDescription { ProductId = 2, ProductName = "DescriptionGoogle Pixel 8 Pro" },
                new ProductDescription { ProductId = 3, ProductName = "Dell XPS 15" },
                new ProductDescription { ProductId = 4, ProductName = "Apple MacBook Air M2" },
                new ProductDescription { ProductId = 5, ProductName = "Apple AirPods Pro (2nd Gen)" },
                new ProductDescription { ProductId = 6, ProductName = "Samsung Galaxy Watch 6" },
                new ProductDescription { ProductId = 7, ProductName = "Amazon Echo Dot (5th Gen)" },
                new ProductDescription { ProductId = 8, ProductName = "DJI Mini 3 Pro Drone" },
                new ProductDescription { ProductId = 9, ProductName = "Logitech MX Master 3S Mouse" },
                new ProductDescription { ProductId = 10, ProductName = "Apple iPhone 15 Pro Max" }
            );

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductDescription)
                .WithOne(pd => pd.Product)
                .HasForeignKey<ProductDescription>(pd => pd.ProductId);

            modelBuilder.Entity<ProductDescription>()
                .HasKey(pd => pd.ProductId); // Explicitly define ProductId as the primary key

            base.OnModelCreating(modelBuilder);
        }
    }
}
