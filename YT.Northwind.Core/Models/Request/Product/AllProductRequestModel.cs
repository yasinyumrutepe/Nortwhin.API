using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Core.Models.Request.Product
{
   public class AllProductRequestModel
    {
        public PaginatedRequest PaginatedRequest { get; set; }
        public string OrderByKey { get; set; }
        public ProductFilter ProductFilterKeys { get; set; }
    }

    public class  ProductFilter 
    {
        public string Categories { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<int> Colors { get; set; }
        public List<int> Sizes { get; set; }
        public string Ratings { get; set; }

    }
}
