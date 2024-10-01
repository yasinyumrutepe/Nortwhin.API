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
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }

            return Ok(_basketService.GetBasket(token));
        }

        [HttpPost]
        public IActionResult AddToBasket(BasketRequestModel basketRequest)
        {

            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }

            var basket = _basketService.AddToBasket(basketRequest, token);
            return Ok(basket);
        }

        [HttpPut("quantity")]
        public IActionResult ChangeQuantity(ChangeQuantityRequestModel changeQuantityRequestModel)
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
            var basket = _basketService.UpdateQuantity(token, changeQuantityRequestModel.ProductID, changeQuantityRequestModel.Quantity);
            return Ok(basket);
        }

        [HttpDelete("{productID}")]
        public IActionResult DeleteFromBasket(int productID)
        {
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }

            var basket = _basketService.DeleteFromBasket(token, productID);
            return Ok(basket);
        }



        [HttpGet("campaign/{campaignName}")]
        public async Task<IActionResult> AddCampaign(string campaignName)
        {

            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
          var basketReq =  await _basketService.AddCampaign(token, campaignName);

            return Ok(basketReq);

        }
    }
}
