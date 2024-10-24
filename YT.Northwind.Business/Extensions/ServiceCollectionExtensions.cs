using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.Business.Filter;


namespace Northwind.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessRegistration(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IProductFavoriteService, ProductFavoriteService>();
            services.AddScoped<IVariantService, VariantService>();

            services.AddScoped<CustomAuthorizationFilter>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("your-secure-key-must-be-at-least-32-characters-long"))
                };
            });

           services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });



        }
    }
}
