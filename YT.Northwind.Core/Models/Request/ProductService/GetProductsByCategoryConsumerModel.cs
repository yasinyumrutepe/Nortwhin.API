

namespace Northwind.Core.Models.Request.ProductService
{
    public class GetProductsByCategoryConsumerModel
    {
        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 0;
        public int CategoryID { get; set; }
        public string CustomerID { get; set; }
    }
}
