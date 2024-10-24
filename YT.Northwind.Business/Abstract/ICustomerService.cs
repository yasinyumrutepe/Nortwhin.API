

using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Customer;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Customer;

namespace Northwind.Business.Abstract
{
    public interface ICustomerService
    {

        Task<PaginatedResponse<CustomerResponseModel>> GetAllCustomersAsync(PaginatedRequest paginated);
        Task<CustomerResponseModel> GetCustomerAsync(string id);
        Task<CustomerResponseModel> GetCustomerByTokenAsync(string token);
        Task<CustomerResponseModel> AddCustomerAsync(CustomerRequestModel customer);
        Task<CustomerResponseModel> UpdateCustomerAsync(CustomerUpdateRequestModel customer);
        Task<int> DeleteCustomerAsync(string id);
       

    }
}
