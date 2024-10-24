using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Response.Order
{
    public class OrderResponseModel
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string ShipAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<OrderStatus>  OrderStatuses { get; set; }





    }
}
