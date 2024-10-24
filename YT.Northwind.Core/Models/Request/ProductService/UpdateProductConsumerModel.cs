
using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Request.ProductService
{
    public class UpdateProductConsumerModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
    }
}
