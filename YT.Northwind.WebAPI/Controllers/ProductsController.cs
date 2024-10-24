using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Product;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Business.Filter;


namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AllProductRequestModel productfilter )
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }

            var products = await _productService.GetAllProductAsync(productfilter, token);


            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
            var product = await _productService.GetProductAsync(id, token);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }


        [HttpGet("category")]

        public async Task<IActionResult> GetProductsByCategory( [FromQuery] CategoryProductsRequest categoryProductsRequest)
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
            var products = await _productService.GetProductsByCategory(categoryProductsRequest, token);
            return Ok(products);
        }



        [HttpPost]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Add([FromForm] ProductRequestModel product)
        {
            var addedProduct = await _productService.AddProductAsync(product);
            return Ok(addedProduct);

        }

        [HttpPut]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequestModel product)
        {
            var updatedProduct = await _productService.UpdateProductAsync(product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<int> Delete(int id)
        {
           return await _productService.DeleteProductAsync(id);
           
        }


    }
}
