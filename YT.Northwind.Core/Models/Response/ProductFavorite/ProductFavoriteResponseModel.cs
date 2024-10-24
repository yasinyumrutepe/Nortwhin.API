

namespace Northwind.Core.Models.Response.ProductFavorite
{
    public class ProductFavoriteResponseModel
    {
        public int ProductFavoriteID { get; set; }
        public int ProductID { get; set; }
        public virtual Entities.Concrete.Product Product { get; set; }

    }
}
