

namespace Northwind.Entities.Concrete
{
    public class ProductReview  
    {
        public int ProductReviewID { get; set; }
        public int ProductID { get; set; }
        public string CustomerID { get; set; }
        public string Review { get; set; }
        public decimal Star { get; set; }
        public int OrderID { get; set; }
    }
}
