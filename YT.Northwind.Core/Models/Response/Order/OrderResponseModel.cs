using Northwind.Core.Models.Response.Customer;
using Northwind.Core.Models.Response.Employee;
using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Response.Order
{
    public class OrderResponseModel
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string ShipAddress { get; set; }
        public virtual CustomerResponseModel Customer { get; set; }
        public virtual EmployeeResponseModel Employee { get; set; }



    }
}
