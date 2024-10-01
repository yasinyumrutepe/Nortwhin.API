

namespace Northwind.Product.Consumer.Models.Request
{
    public class CategoryProductsConsumerRequest
    {
        public int CategoryID { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
