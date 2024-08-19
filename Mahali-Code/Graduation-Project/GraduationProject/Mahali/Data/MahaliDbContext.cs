using Mahali.Models;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Data
{
    public class MahaliDbContext : DbContext
    {
        protected MahaliDbContext() { }
        public MahaliDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<CartProducts> CartProducts { get; set; }
        public DbSet<WishListProducts> WishListProducts { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<ShopOrders> ShopOrders { get; set; }
        public DbSet<ReviewRequest> ReviewRequests { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<LatestProductsVisited> latestProductsVisiteds { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ShopRequest> ShopRequests { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<ProductSizes> ProductSizes { get; set; }
        public DbSet<AboutUs> Abouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Decimal
            modelBuilder.Entity<Cart>()
           .Property(p => p.TotalAmount)
           .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<CartProducts>()
           .Property(p => p.UnitPrice)
           .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
           .Property(p => p.Price)
           .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Order>()
           .Property(p => p.TotalAmount)
           .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderProducts>()
           .Property(p => p.UnitPrice)
           .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Discount>()
           .Property(p => p.DiscountPercentage)
           .HasColumnType("decimal(18, 2)");

            //Enum
            modelBuilder.Entity<CartProducts>()
           .Property(o => o.Size)
           .HasConversion<string>();

            modelBuilder.Entity<CartProducts>()
           .Property(o => o.Color)
           .HasConversion<string>();

            modelBuilder.Entity<ProductColors>()
           .Property(o => o.Color)
           .HasConversion<string>();

            modelBuilder.Entity<ProductSizes>()
           .Property(o => o.Size)
           .HasConversion<string>();

            modelBuilder.Entity<Order>()
           .Property(o => o.TypeOfOrder)
           .HasConversion<string>();

            modelBuilder.Entity<Order>()
          .Property(o => o.TypeOfPayment)
          .HasConversion<string>();

            modelBuilder.Entity<Order>()
           .Property(o => o.Status)
           .HasConversion<string>();

            modelBuilder.Entity<OrderProducts>()
          .Property(o => o.Size)
          .HasConversion<string>();

            modelBuilder.Entity<OrderProducts>()
          .Property(o => o.Color)
          .HasConversion<string>();

            modelBuilder.Entity<ReviewRequest>()
           .Property(o => o.Status)
           .HasConversion<string>();

            modelBuilder.Entity<ShopRequest>()
           .Property(o => o.Status)
           .HasConversion<string>();

        }

    }
}
