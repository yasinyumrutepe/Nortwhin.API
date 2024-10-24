using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class VariantRepository(NorthwindContext context) : GenericRepository<Variant, NorthwindContext>(context), IVariantRepository
    {
    }
}
