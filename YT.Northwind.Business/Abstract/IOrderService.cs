

using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Order;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Order;

namespace Northwind.Business.Abstract
{
    public interface IOrderService
    {

        Task<PaginatedResponse<OrderResponseModel>> GetAllOrdersAsync(PaginatedRequest paginated);
        Task<OrderResponseModel> GetOrderAsync(int id);
        Task<PaginatedResponse<OrderResponseModel>> GetCustomerOrders(string token, PaginatedRequest paginatedRequest);
        Task<OrderResponseModel> AddOrderAsync(string token);
        Task<OrderResponseModel> UpdateOrderAsync(OrderUpdateRequestModel order);
        Task<int> DeleteOrderAsync(int id);


    }
}
