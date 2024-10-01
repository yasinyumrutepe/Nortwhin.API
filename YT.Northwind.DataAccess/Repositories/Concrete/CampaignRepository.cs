

using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class CampaignRepository(NorthwindContext context) : GenericRepository<Campaign, NorthwindContext>(context), ICampaignRepository
    {
    }
}
