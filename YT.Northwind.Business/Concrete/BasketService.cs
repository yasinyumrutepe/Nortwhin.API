using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Basket;
using Northwind.Core.Models.Response.Basket;
using Northwind.Entities.Concrete;
using RTools_NTS.Util;
using StackExchange.Redis;
using System.Security.Cryptography;
using System.Text;

namespace Northwind.Business.Concrete
{
    public class BasketService(IRedisService redisService, ITokenService token,ICampaignService campaignService) : IBasketService
    {
        private readonly IRedisService _redisService = redisService;
        private readonly ITokenService _tokenService = token;
        private readonly ICampaignService _campaignService = campaignService;

        public BasketResponseModel GetAllBasket()
        {
            return _redisService.GetData<BasketResponseModel>("basket");
        }

        #region AddToBasket
        public async Task<BasketResponseModel> AddToBasket(BasketRequestModel basketRequests,string token)
        {
            var customerId = _tokenService.GetCustomerIDClaim(token);

            var basketId = GenerateHashedBasketId(customerId);

            var existingBasket = _redisService.GetData<BasketRequestModel>($"basket:{basketId}");

            if (existingBasket != null)
            {
                // Gelen ürünleri kontrol et
                foreach (var item in basketRequests.Items)
                {
                    var existingItem = existingBasket.Items.FirstOrDefault(i => i.ProductID == item.ProductID);

                    if (existingItem != null)
                    {
                        existingItem.Quantity += item.Quantity;
                    }
                    else
                    {
                        existingBasket.Items.Add(item);
                    }
                }
            }
            else
            {
                basketRequests.BasketID = basketId;
                existingBasket = basketRequests;
            }

            _redisService.SetData($"basket:{basketId}", existingBasket);

            var response = new BasketResponseModel
            {
                BasketId = basketId,
                TotalPrice = existingBasket.TotalPrice,
                Items = existingBasket.Items,
                Message = "Product(s) successfully added to the basket."
            };

            return await Task.FromResult(response);



        }
        #endregion

        #region GetBasket
        public BasketRequestModel GetBasket(string token)
        {
            var customerId = _tokenService.GetCustomerIDClaim(token);

            var basketId = GenerateHashedBasketId(customerId);
            var basket = _redisService.GetData<BasketRequestModel>($"basket:{basketId}");
            return basket;

        }
        #endregion

        #region UpdateQuantity
        public BasketRequestModel UpdateQuantity(string token,int productID,int quantity)
        {

            var customerId = _tokenService.GetCustomerIDClaim(token);

            var basketId = GenerateHashedBasketId(customerId);
            var existingBasket = _redisService.GetData<BasketRequestModel>($"basket:{basketId}");

            if (existingBasket != null)
            {
                var itemToUpdate = existingBasket.Items.FirstOrDefault(i => i.ProductID == productID);

                if (itemToUpdate != null)
                {
                    itemToUpdate.Quantity = quantity;
                    _redisService.SetData($"basket:{basketId}", existingBasket);
                    return _redisService.GetData<BasketRequestModel>($"basket:{basketId}");
                }
            }

            return _redisService.GetData<BasketRequestModel>($"basket:{basketId}");
        }
        #endregion

        #region DeleteFromBasket
        public BasketRequestModel DeleteFromBasket(string token,int productID)
        {

            var customerId = _tokenService.GetCustomerIDClaim(token);

            var basketId = GenerateHashedBasketId(customerId);
            var existingBasket = _redisService.GetData<BasketRequestModel>($"basket:{basketId}");

            if (existingBasket != null)
            {
               
                var itemToRemove = existingBasket.Items.FirstOrDefault(i => i.ProductID == productID);

                if (itemToRemove != null)
                {
                   
                    existingBasket.Items.Remove(itemToRemove);

                    
                    _redisService.SetData($"basket:{basketId}", existingBasket);
                   
                    return _redisService.GetData<BasketRequestModel>($"basket:{basketId}");
                }
            }

            return _redisService.GetData<BasketRequestModel>($"basket:{basketId}");
        }
        #endregion

        public async Task<BasketRequestModel> AddCampaign(string token, string campaignName)
        {   
         
            var customerId = _tokenService.GetCustomerIDClaim(token);

            var basketId = GenerateHashedBasketId(customerId);

            var existingBasket = _redisService.GetData<BasketRequestModel>($"basket:{basketId}");
            if (existingBasket == null)
            {
                return new BasketRequestModel();
            }
            var campaing = await _campaignService.FindCampaignAsync(campaignName);
            if (campaing == null)
            {
                existingBasket.Discount = null;
                _redisService.SetData($"basket:{basketId}", existingBasket);

                return existingBasket;
            }
            existingBasket.Discount = new BasketDiscount
            {
                CampaignName = campaing.CampaignName,
                DiscountAmount = campaing.DiscountAmount,
                IsPercent = campaing.IsPercent
            };

            Console.WriteLine(existingBasket);

            _redisService.SetData($"basket:{basketId}", existingBasket);

            return existingBasket;



        }


        public int ClearBasket(string token)
        {
            var customerId = _tokenService.GetCustomerIDClaim(token);
            var basketId = GenerateHashedBasketId(customerId);

            _redisService.Delete<BasketRequestModel>($"basket:{basketId}");
            return 1;
        }

        private static string GenerateHashedBasketId(string customerId)
        {
            var bytes = Encoding.UTF8.GetBytes(customerId);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

       
    }
}
