
namespace Northwind.Core.Models.Request.ProductReview
{
    public class ProductReviewRequestModel
    {
        public int ProductID { get; set; }
        public string Review { get; set; }
        public float Star { get; set; }
    }
}
