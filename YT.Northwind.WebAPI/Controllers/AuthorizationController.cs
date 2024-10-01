using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Filter;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult ShowPage()
        {
            return Ok("You are authorized to see this page");
        }
    }
}
