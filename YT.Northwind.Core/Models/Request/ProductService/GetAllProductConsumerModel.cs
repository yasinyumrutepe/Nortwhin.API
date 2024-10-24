
namespace Northwind.Core.Models.Request.ProductService
{
    public class GetAllProductConsumerModel
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string CustomerID { get; set; }

    }
}
