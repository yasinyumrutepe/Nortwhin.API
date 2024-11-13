using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Request.Product
{
    public class ProductUpdateRequestModel
    {
        public int ProductID { get; set; }
        public ICollection<int> Categories { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public ICollection<int> Sizes { get; set; }
        public int Colors { get; set; }


    }
}
