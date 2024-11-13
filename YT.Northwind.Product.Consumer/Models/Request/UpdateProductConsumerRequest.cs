using Northwind.Entities.Concrete;
using System.Text.Json.Serialization;

namespace Northwind.Product.Consumer.Models.Request
{
    public class UpdateProductConsumerRequest
    {
        public int ProductID { get; set; }
        public ICollection<int> Categories { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public ICollection<int> Sizes { get; set; }
        public int Color { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
