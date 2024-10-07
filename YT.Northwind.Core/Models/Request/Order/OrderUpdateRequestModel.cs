using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Request.Order
{
    public class OrderUpdateRequestModel
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public int? OrderStatusID { get; set; } // Nullable
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; } // Nullable
        public DateTime? RequiredDate { get; set; } // Nullable
        public DateTime? ShippedDate { get; set; } // Nullable
        public decimal? Freight { get; set; } // Nullable
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public string ShipAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

    }
}
