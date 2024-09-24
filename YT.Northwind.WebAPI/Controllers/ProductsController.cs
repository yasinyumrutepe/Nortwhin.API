using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Product;



namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedRequest paginatedRequest )
        {
            var products = await _productService.GetAllProductAsync(paginatedRequest);


            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        [HttpPost]

        public async Task<IActionResult> Add([FromForm] ProductRequestModel product)
        {
            var addedProduct = await _productService.AddProductAsync(product);
            return Ok(addedProduct);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequestModel product)
        {
            var updatedProduct = await _productService.UpdateProductAsync(product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]

        public async Task<int> Delete(int id)
        {
           return await _productService.DeleteProductAsync(id);
           
        }


    }
}
