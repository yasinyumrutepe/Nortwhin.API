using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.ProductImage;
using Northwind.Entities.Concrete;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpPost()]
        public async Task<IActionResult> UploadProductImage([FromForm] ProductImageRequestModel productImageRequest)
        {
            var result = await _productImageService.InsertProductImageAsync(productImageRequest);
            if (result == null) return BadRequest();
            return Ok(result);
        }



        [HttpDelete("{productImageID}")]
        public async Task<IActionResult> DeleteProductImage(int productImageID)
        {

            var result = await _productImageService.DeleteProductImageAsync(productImageID);
            if (result != 1) return NotFound();

            return Ok(new { success = true ,imageID = productImageID });
        }
    }
}
