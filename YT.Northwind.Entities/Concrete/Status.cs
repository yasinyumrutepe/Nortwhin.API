
namespace Northwind.Entities.Concrete
{
    public class Status
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int StatusNumber { get; set; }
        public string Color { get; set; }
        public ICollection<OrderStatus> OrderStatuses { get; set; }
    }
}
