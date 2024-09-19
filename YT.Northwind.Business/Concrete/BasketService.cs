using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Basket;
using Northwind.Core.Models.Response.Basket;

namespace Northwind.Business.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IRedisService _redisService;
        private readonly IProductService _productService;

        public BasketService(IRedisService redisService, IProductService productService)
        {
            _redisService = redisService;
            _productService = productService;
        }


        public BasketResponseModel GetAllBasket()
        {
            return _redisService.GetData<BasketResponseModel>("basket");
        }
        public async Task<BasketResponseModel> AddToBasket(List<BasketRequestModel> basketRequests)
        {
            var redisBasketData = _redisService.GetData<List<BasketRequestModel>>("basket");
            if (redisBasketData == null)
            {
               
                _redisService.SetData("basket", basketRequests);
            }else
            {

                basketRequests.ForEach(x =>
                {
                    var product = redisBasketData.FirstOrDefault(y => y.ProductID == x.ProductID);
                    if (product == null)
                    {
                        redisBasketData.Add(x);
                    }
                    else
                    {
                        product.Quantity += x.Quantity;
                    }
                });
                _redisService.SetData("basket", redisBasketData);



            }
            var basketResponse = new BasketResponseModel();
            var returnBasketData = _redisService.GetData<List<BasketRequestModel>>("basket");

            foreach (var item in returnBasketData)
            {
                var product = await _productService.GetProductAsync(item.ProductID);
                basketResponse.Products.Add(new BasketCacheModel
                {
                    Product = product,
                    Quantity = item.Quantity
                });
                basketResponse.TotalPrice += product.UnitPrice * item.Quantity;
            }          

            return basketResponse;
        }

        public void DeleteFromBasket(int basketID)
        {
            _redisService.Delete<BasketRequestModel>("basket");
        }

        public BasketResponseModel GetBasket(Guid basketID)
        {
            return  _redisService.GetData<BasketResponseModel>("basket");
            
        }

        public void UpdateBasket(BasketRequestModel basketRequests)
        {   
          



            _redisService.Delete<BasketRequestModel>("basket");
            _redisService.SetData("basket", basketRequests);

        }
    }
}
