namespace 
    Northwind.Core.Models.Request.Category
{
    public class CategoryRequestModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategoryRequestModel> List { get; set; }
    }

    public class SubCategoryRequestModel
    {
        
        public string SubCategoryName { get; set; }
    }
}
