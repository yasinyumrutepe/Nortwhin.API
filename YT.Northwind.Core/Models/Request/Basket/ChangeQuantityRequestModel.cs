using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Core.Models.Request.Basket
{
    public class ChangeQuantityRequestModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
