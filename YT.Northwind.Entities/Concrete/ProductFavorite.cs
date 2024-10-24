

using System.Text.Json.Serialization;

namespace Northwind.Entities.Concrete
{
    public class ProductFavorite
    {
        public int ProductFavoriteID { get; set; }
        public int ProductID { get; set; }
        public string CustomerID { get; set; }
        public Product Product { get; set; }
    }
}
