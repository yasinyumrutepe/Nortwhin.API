using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Basket;
using Northwind.Core.Models.Response.Basket;
using Stripe;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Northwind.Business.Concrete
{
    public class BasketService(IRedisService redisService,ICampaignService campaignService) : IBasketService
    {
        private readonly IRedisService _redisService = redisService;
        private readonly ICampaignService _campaignService = campaignService;

        public BasketResponseModel GetAllBasket()
        {
            return _redisService.GetData<BasketResponseModel>("basket");
        }

        #region AddToBasket
        public async Task<BasketResponseModel> AddToBasket(BasketRequestModel basketRequests,string ipAddress="")
        {
            string basketID = GenerateHashedBasketId(ipAddress);
            var existingBasket = _redisService.GetData<BasketRequestModel>($"basket:{basketID}");
            if (existingBasket != null)
            {
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
               

                basketRequests.BasketID = basketID;
                existingBasket = basketRequests;
            }

            _redisService.SetData($"basket:{basketID}", existingBasket);

            var response = new BasketResponseModel
            {
                BasketID = basketID,
                TotalPrice = existingBasket.TotalPrice,
                Items = existingBasket.Items,
                Message = "Product(s) successfully added to the basket."
            };

            return await Task.FromResult(response);



        }
        #endregion

        #region GetBasket
        public BasketRequestModel GetBasket(string ipAddress="")
        {
            string basketID = GenerateHashedBasketId(ipAddress);
            return _redisService.GetData<BasketRequestModel>($"basket:{basketID}");
           
        }
        #endregion

        #region UpdateQuantity
        public BasketRequestModel UpdateQuantity(string ipAddress,int productID=0,int quantity=0)
        {
            string basketId = GenerateHashedBasketId(ipAddress);
          
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
        public BasketRequestModel DeleteFromBasket(string ipAddress = "",int productID=0)
        {
            string basketId = GenerateHashedBasketId(ipAddress);
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

        #region AddCampaign
        public async Task<BasketRequestModel> AddCampaign(string ipAddress = "",  string campaignName="")
        {
            string basketId = GenerateHashedBasketId(ipAddress);

          
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
                _redisService.SetData($"basket:{basketId}", existingBasket);
                return existingBasket;
        }

        #endregion

        public int ClearBasket(string ipAddress = "")
        {
            string basketId = GenerateHashedBasketId(ipAddress);
            _redisService.Delete<BasketRequestModel>($"basket:{basketId}");
            return 1;
        }

        private static string GenerateHashedBasketId(string ipAddress)
        {
            var bytes = Encoding.UTF8.GetBytes(ipAddress);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

       
    }
}
