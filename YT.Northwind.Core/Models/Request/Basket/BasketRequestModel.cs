namespace 
    Northwind.Core.Models.Request.Basket
{
    public class BasketRequestModel
    {
        public Guid BasketID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

    }


}


