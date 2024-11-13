
using Northwind.Core.Models.Response.Cloudinary;

namespace Northwind.Core.Models.Request.ProductService
{
    public class CreateProductConsumerModel
    {
        public string ProductName { get; set; }
        public ICollection<int> Categories { get; set; }
        public decimal UnitPrice { get; set; }
        public string QuantityPerUnit { get; set; }
        public string Description { get; set; }
        public ICollection<int> Size { get; set; }
        public int Color { get; set; }
        public ICollection<UploadImageResponseModel> Images { get; set; }

    }
}
