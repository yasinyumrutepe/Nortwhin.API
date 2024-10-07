

namespace Northwind.Core.Models.Request.Order
{
    public class ChangeOrderStatusRequestModel
    {
        public int OrderID { get; set; }
        public int OrderStatusID { get; set; }
    }
}
