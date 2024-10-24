using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;

namespace Northwind.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VariantController(IVariantService variantService) : Controller
    {
        private readonly IVariantService _variantService = variantService;

        [HttpGet]
        public async Task<IActionResult> GetAllVariants()
        {
            var variants = await _variantService.GetAllVariant();
            return Ok(variants);

        }
    }
}
