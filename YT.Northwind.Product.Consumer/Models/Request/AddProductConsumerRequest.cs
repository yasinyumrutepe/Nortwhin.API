using Northwind.Core.Models.Response.Cloudinary;
using System;


namespace Northwind.Product.Consumer.Models.Request
{
    public class AddProductConsumerRequest
    {
     
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public List<int> Size { get; set; }
        public int Color { get; set; }
        public List<UploadImageResponseModel> ProductImages { get; set; }



    }
}
