

namespace Northwind.Core.Models.Request.Order
{
    public class ChangeOrderStatusRequestModel
    {
        public int OrderID { get; set; }
        public int StatusID { get; set; }
    }
}
