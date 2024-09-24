﻿using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=northwind;Integrated Security=True"));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();




        }

    }
}