
namespace Northwind.Entities.Concrete
{
    public class User 
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
