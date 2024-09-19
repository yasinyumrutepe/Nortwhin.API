using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class EmployeeRepository(NorthwindContext context) : GenericRepository<Employee,NorthwindContext>(context), IEmployeeRepository
    {
    }
}
