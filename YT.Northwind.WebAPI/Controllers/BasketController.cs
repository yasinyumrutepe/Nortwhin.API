using Microsoft.AspNetCore.Mvc;
using 
    
    
    Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Basket;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {   
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public IActionResult GetAllBasket()
        {
            return Ok(_basketService.GetAllBasket());

        }

        [HttpGet("{basketId}")]
        public IActionResult GetBasket(Guid basketId)
        {
          return Ok(  _basketService.GetBasket(basketId));
        }

        [HttpPost]
        public IActionResult AddToBasket(List<BasketRequestModel> basketRequest)
        {
          var basket =  _basketService.AddToBasket(basketRequest);
            return Ok(basket);
        }
    }
}
