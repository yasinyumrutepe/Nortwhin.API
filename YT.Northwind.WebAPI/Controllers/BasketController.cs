using Microsoft.AspNetCore.Mvc;
using 
    
    
    Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Basket;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController(IBasketService basketService) : Controller
    {
        private readonly IBasketService _basketService = basketService;

        [HttpGet]
        public IActionResult GetAllBasket()
        {
            return Ok(_basketService.GetAllBasket());

        }

        [HttpGet("detail")]
        public IActionResult GetBasket()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString() ?? "127.0.0.1";
            var basket = _basketService.GetBasket(ipAddress);
            return Ok(basket);
        }

        [HttpPost]
        public IActionResult AddToBasket(BasketRequestModel basketRequest)
        {
            
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString() ?? "127.0.0.1";

            var basket = _basketService.AddToBasket(basketRequest, ipAddress);
            return Ok(basket);
        }

        [HttpPut("quantity")]
        public IActionResult ChangeQuantity(ChangeQuantityRequestModel changeQuantityRequestModel)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString() ?? "127.0.0.1";
            var basket = _basketService.UpdateQuantity(ipAddress, changeQuantityRequestModel.ProductID, changeQuantityRequestModel.Quantity);
            return Ok(basket);
        }

        [HttpDelete]
        public IActionResult DeleteFromBasket([FromQuery]int productID)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString() ?? "127.0.0.1";
            var basket = _basketService.DeleteFromBasket(ipAddress, productID);
            return Ok(basket);
        }



        [HttpGet("campaign")]
        public async Task<IActionResult> AddCampaign([FromQuery] string campaignName)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString() ?? "127.0.0.1";
            var basketReq =  await _basketService.AddCampaign(ipAddress,  campaignName );

            return Ok(basketReq);

        }
    }
}
