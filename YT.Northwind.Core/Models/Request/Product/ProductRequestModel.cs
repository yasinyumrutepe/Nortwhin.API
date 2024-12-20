﻿using Microsoft.AspNetCore.Http;
using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Request.Product
{
    public class ProductRequestModel
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public string Size { get; set; }
        public int Color { get; set; }
        public IFormFile[] Images { get; set; }
        public string Categories { get; set; }
    }
}
