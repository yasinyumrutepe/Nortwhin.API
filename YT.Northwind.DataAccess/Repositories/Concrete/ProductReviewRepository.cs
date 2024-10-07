
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class ProductReviewRepository(NorthwindContext context) : GenericRepository<ProductReview, NorthwindContext>(context), IProductReviewRepository
    {
    }
   
}
