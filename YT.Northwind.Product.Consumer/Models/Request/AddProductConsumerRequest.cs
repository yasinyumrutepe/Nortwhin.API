using Northwind.Core.Models.Response.Cloudinary;
using System;


namespace Northwind.Product.Consumer.Models.Request
{
    public class AddProductConsumerRequest
    {
     
        public string ProductName { get; set; }
        public ICollection<int>  Categories { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public ICollection<int> Size { get; set; }
        public int Color { get; set; }
        public ICollection<UploadImageResponseModel> ProductImages { get; set; }



    }
}
