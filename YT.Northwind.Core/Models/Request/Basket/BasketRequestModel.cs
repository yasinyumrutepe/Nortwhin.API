using Northwind.Entities.Concrete;

namespace 
    Northwind.Core.Models.Request.Basket
{
    public class BasketRequestModel
    {
        public string BasketID { get; set; } 
        public List<BasketItem> Items { get; set; }

        public decimal TotalPrice
        {
            get
            {
                if (Discount == null) 
                   return Items.Sum(item => item.TotalPrice);

                return Discount.IsPercent
                    ? Items.Sum(item => item.TotalPrice) * (1 - Discount.DiscountAmount / 100)
                    : Items.Sum(item => item.TotalPrice) - Discount.DiscountAmount;
            }
        }


        public BasketDiscount Discount { get; set; }

    }

    public class BasketItem
    {
        public int ProductID { get; set; } 
        public string ProductName { get; set; } 
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; } 
        public decimal UnitPrice { get; set; } 
        public int Quantity { get; set; }


        public decimal TotalPrice => UnitPrice * Quantity;
        public List<Entities.Concrete.ProductImage> Images { get; set; } 
      
    }

    public class BasketDiscount
    {
        public string CampaignName { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsPercent { get; set; }
    }

}


