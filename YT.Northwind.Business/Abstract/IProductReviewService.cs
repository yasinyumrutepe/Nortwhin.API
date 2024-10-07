

using Northwind.Core.Models.Request.ProductReview;
using Northwind.Core.Models.Response.ProductReview;

namespace Northwind.Business.Abstract
{
    public interface IProductReviewService
    {
        Task<ProductReviewResponseModel> AddProductRewiewAsync(ProductReviewRequestModel productRewiewRequestModel,string token);


    }
}
