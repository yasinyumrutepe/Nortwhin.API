

namespace Northwind.Core.Models.Request.ProductService
{
    public class CategoryProductsRequest
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string Slug { get; set; }
    }
}
