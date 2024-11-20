using Northwind.Core.Models.Request.Basket;
using Northwind.Core.Models.Response.Basket;

namespace Northwind.Business.Abstract
{
    public interface IBasketService
    {
        public BasketResponseModel GetAllBasket();
        public BasketRequestModel GetBasket(string ipAddress);

        public Task<BasketResponseModel> AddToBasket(BasketRequestModel basketRequests,string ipAddress);

        public BasketRequestModel DeleteFromBasket(string ipAddress, int productID);

        public int ClearBasket(string ipAddress);

        public BasketRequestModel UpdateQuantity(string ipAddress , int productID, int quantity);

        public Task<BasketRequestModel> AddCampaign(string ipAddress, string campaignName);




    }
}
