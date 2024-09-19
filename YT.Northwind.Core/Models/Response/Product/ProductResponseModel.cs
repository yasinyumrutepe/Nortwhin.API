

using Northwind.Core.Models.Response.Category;

namespace Northwind.Core.Models.Response.Product
{
    public class ProductResponseModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
   
    }
}
