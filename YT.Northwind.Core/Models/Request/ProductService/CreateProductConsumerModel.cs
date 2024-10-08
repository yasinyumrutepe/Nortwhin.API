﻿
using Northwind.Core.Models.Response.Cloudinary;

namespace Northwind.Core.Models.Request.ProductService
{
    public class CreateProductConsumerModel
    {
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public string QuantityPerUnit { get; set; }
        public string Description { get; set; }
        public List<UploadImageResponseModel> Images { get; set; }

    }
}
