
namespace 
    Northwind.Entities.Concrete
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int MainCategoryID { get; set; }
        public string Slug { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
