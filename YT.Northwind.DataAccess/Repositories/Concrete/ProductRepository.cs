using Microsoft.EntityFrameworkCore;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class ProductRepository(NorthwindContext context) : GenericRepository<Product, NorthwindContext>(context), IProductRepository
    {
    }
}
