using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.ProductReview;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController(IProductReviewService productRewiewService) : Controller
    {

        private readonly IProductReviewService _productReviewService = productRewiewService;

        [HttpPost]
        public async Task<IActionResult> AddProductRewiew([FromBody] ProductReviewRequestModel rewiewRequestModel )
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }

            var review = await _productReviewService.AddProductRewiewAsync(rewiewRequestModel, token);

            return Ok(review);
          
        }
    }
}
