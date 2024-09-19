using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Core.Models.Response;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class OrderRepository(NorthwindContext context) : GenericRepository<Order, NorthwindContext>(context), IOrderRepository
    {   
      

      
    }

}
