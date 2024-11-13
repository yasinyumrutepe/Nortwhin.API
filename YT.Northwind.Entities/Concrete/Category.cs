namespace Northwind.Entities.Concrete
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int? Parent_ID { get; set; }
        public string Slug { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
