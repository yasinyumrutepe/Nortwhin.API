using Northwind.Core.Models.Request.Basket;
using Northwind.Core.Models.Response.Basket;

namespace Northwind.Business.Abstract
{
    public interface IBasketService
    {
        public BasketResponseModel GetAllBasket();
        public BasketRequestModel GetBasket(string token);

        public Task<BasketResponseModel> AddToBasket(BasketRequestModel basketRequests,string token);

        public BasketRequestModel DeleteFromBasket(string token,int productID);

        public int ClearBasket(string token);

        public BasketRequestModel UpdateQuantity(string token, int productID, int quantity);

        public Task<BasketRequestModel> AddCampaign(string token, string campaignName);




    }
}
