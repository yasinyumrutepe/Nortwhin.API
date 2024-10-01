

namespace Northwind.Core.Models.Request.ProductService
{
    public class CategoryProductsRequest
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int CategoryID { get; set; }
    }
}
