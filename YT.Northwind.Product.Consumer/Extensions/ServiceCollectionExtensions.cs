

using Microsoft.Extensions.DependencyInjection;
using Northwind.Product.Consumer.Abstract;
using Northwind.Product.Consumer.Concrete;

namespace Northwind.Product.Consumer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProductServiceRegisration(this IServiceCollection services)
        {
            services.AddScoped<IProductConsumerService, ProductConsumerService>();
        }
    }
}
