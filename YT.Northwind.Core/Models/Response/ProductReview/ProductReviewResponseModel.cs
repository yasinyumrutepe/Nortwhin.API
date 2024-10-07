

namespace Northwind.Core.Models.Response.ProductReview
{
    public class ProductReviewResponseModel
    {
        public int ProductReviewID { get; set; }
        public int ProductID { get; set; }
        public string CustomerID { get; set; }
        public string Review { get; set; }
        public float Star { get; set; }

    }
}
