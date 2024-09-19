using Northwind.Core.Models.Request.Basket;
using Northwind.Core.Models.Response.Basket;

namespace Northwind.Business.Abstract
{
    public interface IBasketService
    {
        public BasketResponseModel GetAllBasket();
        public BasketResponseModel GetBasket(Guid basketID);

        public Task<BasketResponseModel> AddToBasket(List<BasketRequestModel> basketRequests);

        public void DeleteFromBasket(int basketID);

        public void UpdateBasket(BasketRequestModel basketRequests);



    }
}
