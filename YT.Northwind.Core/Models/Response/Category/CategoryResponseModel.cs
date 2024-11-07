
namespace Northwind.Core.Models.Response.Category
{
    public class CategoryResponseModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int MainCategoryID { get; set; }
        public string Slug { get; set; }

    }
}
