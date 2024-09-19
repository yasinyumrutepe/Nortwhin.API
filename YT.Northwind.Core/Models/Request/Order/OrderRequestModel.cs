namespace Northwind.Core.Models.Request.Order
{
    public class OrderRequestModel
    {
        public string Token { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public virtual ICollection<OrderDetailRequestModel> OrderDetails { get; set; }


    }
}
