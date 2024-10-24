using Northwind.Entities.Concrete;


namespace Northwind.Product.Consumer.Models.Response
{
    public class ProductConsumerResponse
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
