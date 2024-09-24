﻿

using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Response.Product
{
    public class ProductResponseModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; } 
        public virtual ICollection<ProductImage> ProductImages { get; set; }


    }
}
