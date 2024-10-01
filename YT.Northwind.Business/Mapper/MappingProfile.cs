using AutoMapper;
using Northwind.Core.Models.Request.Campaign;
using Northwind.Core.Models.Request.Category;
using Northwind.Core.Models.Request.Customer;
using Northwind.Core.Models.Request.Employee;
using Northwind.Core.Models.Request.Order;
using Northwind.Core.Models.Request.Product;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Auth;
using Northwind.Core.Models.Response.Campaign;
using Northwind.Core.Models.Response.Category;
using Northwind.Core.Models.Response.Customer;
using Northwind.Core.Models.Response.Employee;
using Northwind.Core.Models.Response.Order;
using Northwind.Core.Models.Response.Product;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //ProductRequestModel to Product
            CreateMap<ProductRequestModel,Product>();
            CreateMap<Product, ProductResponseModel>();
            CreateMap<PaginatedResponse<Product>, PaginatedResponse<ProductResponseModel>>();
            CreateMap<ProductUpdateRequestModel,Product>();

            //CategoryRequestModel to Category
            CreateMap<CategoryRequestModel,Category>();
            CreateMap<Category, CategoryResponseModel>();
            CreateMap<PaginatedResponse<Category>, PaginatedResponse<CategoryResponseModel>>();
            CreateMap<CategoryUpdateRequestModel,Category>();


            //EmployeeRequestModel to Employee
            CreateMap<Employee, EmployeeResponseModel>();
            CreateMap<EmployeeRequestModel, Employee>();
            CreateMap<PaginatedResponse<Employee>, PaginatedResponse<EmployeeResponseModel>>();
            CreateMap<EmployeeUpdateRequestModel, Employee>();

            //CustomerRequestModel to Customer
            CreateMap<CustomerRequestModel,Customer>();
            CreateMap<Customer, CustomerResponseModel>();
            CreateMap<PaginatedResponse<Customer>, PaginatedResponse<CustomerResponseModel>>();
            CreateMap<CustomerUpdateRequestModel, Customer>();

            //OrderRequestModel to Order
            CreateMap<OrderRequestModel, Order>();
            CreateMap<Order, OrderResponseModel>();
            CreateMap<PaginatedResponse<Order>, PaginatedResponse<OrderResponseModel>>();
            CreateMap<OrderUpdateRequestModel, Order>();


            //OrderDetailRequestModel to OrderDetail
            CreateMap<OrderDetailRequestModel, OrderDetail>();

            //UserResponseModel to User
            CreateMap<User, UserResponseModel>();
            //RegisterRequestModel to User


            //CampaignRequestModel to Campaign
           CreateMap<CampaignRequestModel, Campaign>();
            CreateMap<Campaign, CampaignResponseModel>();






        }

    }
}
