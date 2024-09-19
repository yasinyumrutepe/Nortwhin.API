using 
    
    
    Northwind.Core.Models.Response.Product;
using Northwind.Entities.Concrete;

namespace Northwind.Core.Models.Response.Category
{
    public class CategoryResponseModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
     
    }
}
