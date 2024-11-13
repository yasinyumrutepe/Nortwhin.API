using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Concrete.EntityFramework
{
    public  class NorthwindContext(DbContextOptions<NorthwindContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductFavorite> ProductFavorites { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       

    
            modelBuilder.Entity<OrderDetail>().HasKey(x => new { x.OrderID, x.ProductID });
            modelBuilder.Entity<ProductFavorite>().HasKey(x => x.ProductFavoriteID);
            modelBuilder.Entity<OrderStatus>()
           .HasKey(os => new { os.OrderStatusID }); 

            modelBuilder.Entity<OrderStatus>()
                .HasOne(os => os.Order)
                .WithMany(o => o.OrderStatuses)
                .HasForeignKey(os => os.OrderID);

            modelBuilder.Entity<OrderStatus>()
                .HasOne(os => os.Status)
                .WithMany(s => s.OrderStatuses)
                .HasForeignKey(os => os.StatusID);

            modelBuilder.Entity<ProductCategory>()
            .Property(p => p.ProductCategoryID)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductID);

            modelBuilder.Entity<ProductVariant>()
                .Property (p => p.ProductVariantID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ProductVariant>()
                .HasOne(pc=>pc.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(pc => pc.ProductID);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


    }
}
