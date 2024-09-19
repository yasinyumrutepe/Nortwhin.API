

using 
    Northwind.Core.Models.Response.Product;

namespace Northwind.Core.Models.Response.Basket
{
    public class BasketResponseModel
    {
        public Guid BasketID { get; set; }
        public List<BasketCacheModel> Products { get; set; } = new();
        public decimal TotalPrice { get; set; }

    }


    public class  BasketCacheModel
    {
        public ProductResponseModel Product { get; set; }
        public int Quantity { get; set; }

    }


}
