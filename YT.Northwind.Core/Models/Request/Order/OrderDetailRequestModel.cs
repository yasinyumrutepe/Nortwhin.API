namespace Northwind.Core.Models.Request.Order
{
    public class OrderDetailRequestModel
    {
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}
