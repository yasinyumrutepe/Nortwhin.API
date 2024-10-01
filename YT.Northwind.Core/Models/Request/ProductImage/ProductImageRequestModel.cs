using Microsoft.AspNetCore.Http;

namespace Northwind.Core.Models.Request.ProductImage
{
    public class ProductImageRequestModel
    {
        public int ProductID { get; set; }
        public IFormFile[] Images { get; set; }
    }
}
