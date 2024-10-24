

using Northwind.Entities.Concrete;
using System.Drawing;

namespace Northwind.Core.Models.Response.Product
{
    public class ProductResponseModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Entities.Concrete.ProductReview> ProductReviews { get; set; }
        public virtual ICollection<Entities.Concrete.ProductFavorite> ProductFavorites { get; set; }
        public virtual ICollection<Entities.Concrete.ProductVariant> ProductVariants { get; set; }


    }
}
