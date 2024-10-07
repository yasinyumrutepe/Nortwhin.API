using System;

namespace Northwind.Core.Models.Request.Payment
{
    public class PaymentRequestModel
    {
        public long Amount { get; set; }
        public string currency { get; set; }
        public List<string> PaymentMethodTypes { get; set; }
    }
}
