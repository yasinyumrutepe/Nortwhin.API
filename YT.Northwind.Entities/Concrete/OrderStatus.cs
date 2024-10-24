
namespace Northwind.Entities.Concrete
{
    public class OrderStatus
    {
        public int OrderStatusID { get; set; }
        public int OrderID { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Order Order  { get; set; }
        public virtual Status Status { get; set; }


    }
}
