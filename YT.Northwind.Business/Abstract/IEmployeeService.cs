
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Employee;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Employee;

namespace Northwind.Business.Abstract
{
    public interface IEmployeeService
    {
        Task<PaginatedResponse<EmployeeResponseModel>> GetAllEmployeesAsync(PaginatedRequest paginated);
        Task<EmployeeResponseModel> GetEmployeeAsync(int id);
        Task<EmployeeResponseModel> AddEmployeeAsync(EmployeeRequestModel employee);
        Task<EmployeeResponseModel> UpdateEmployeeAsync(EmployeeUpdateRequestModel employee);
        Task<int> DeleteEmployeeAsync(int id);
    }
}
