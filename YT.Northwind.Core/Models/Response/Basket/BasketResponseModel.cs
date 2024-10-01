

using Northwind.Core.Models.Request.Basket;


namespace Northwind.Core.Models.Response.Basket
{
    public class BasketResponseModel
    {
        public string BasketId { get; set; } 
        public decimal TotalPrice { get; set; } 
        public List<BasketItem> Items { get; set; } 
        public string Message { get; set; } 
    }

}
