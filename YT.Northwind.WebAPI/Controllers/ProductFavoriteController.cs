using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.ProductFavorite;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFavoriteController : Controller
    {
        private readonly IProductFavoriteService _productFavoriteService;

        public ProductFavoriteController(IProductFavoriteService productFavoriteService)
        {
            _productFavoriteService = productFavoriteService;

        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerFavorites()
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
            var productFavorites = await _productFavoriteService.GetCustomerFavorites(token);
            return Ok(productFavorites);
        }



        [HttpPost]

        public async Task<IActionResult> AddProductFavorite([FromBody] ProductFavoriteRequestModel productFavoriteRequestModel )
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
            return Ok( await  _productFavoriteService.AddProductFavoriteAsync(productFavoriteRequestModel, token));
        }

        [HttpDelete("{productID}")]

        public async Task<IActionResult> DeleteProductFavorite(int productID)
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
            var isDeleted = await _productFavoriteService.DeleteProductFavoriteAsync(productID, token);
            return Ok(isDeleted);
        }
    }
}
