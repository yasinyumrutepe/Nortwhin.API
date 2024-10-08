﻿using Microsoft.AspNetCore.Http;

namespace Northwind.Core.Models.Request.Product
{
    public class ProductRequestModel
    {
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public string QuantityPerUnit { get; set; }
        public string Description { get; set; }
        public IFormFile[] Images { get; set; }
    }
}
