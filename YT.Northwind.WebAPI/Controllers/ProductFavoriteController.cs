using Microsoft.AspNetCore.Mvc;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFavoriteController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("adawd");
        }
    }
}
