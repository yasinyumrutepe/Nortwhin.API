using 
    Northwind.Core.Models.Response.Auth;
using Northwind.Core.Models.Response.Order;
using Northwind.Core.Models.Response.ProductFavorite;
using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Response.Customer
{
    public class CustomerResponseModel
    {

        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public ICollection<OrderResponseModel> Orders { get; set; }
        public ICollection<ProductFavoriteResponseModel>  ProductFavorites { get; set; }
        public UserResponseModel User { get; set; }
    }
}
