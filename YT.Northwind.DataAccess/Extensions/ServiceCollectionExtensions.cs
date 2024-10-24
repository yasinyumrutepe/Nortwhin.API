using Microsoft.Extensions.DependencyInjection;
using Northwind.DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.DataAccess.Repositories.Concrete;

namespace Northwind.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessRegisration(this IServiceCollection services)
        {
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer("Data Source=localhost,1433;Initial Catalog=northwind;User ID=sa;Password=Yy654321**;TrustServerCertificate=True;", sqlOptions => sqlOptions.CommandTimeout(180)));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
            services.AddScoped<IProductFavoriteRepository, ProductFavoriteRepository>();
            services.AddScoped<IVariantRepository, VariantRepository>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();




        }

    }
}
