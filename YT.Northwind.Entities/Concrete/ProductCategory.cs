
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Entities.Concrete
{
    public class ProductCategory
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int ProductCategoryID { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
