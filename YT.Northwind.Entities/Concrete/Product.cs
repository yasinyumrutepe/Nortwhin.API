﻿using System.Text.Json.Serialization;

namespace Northwind.Entities.Concrete
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }

    }
}