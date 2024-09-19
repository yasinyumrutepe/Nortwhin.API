
using AutoMapper;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Order;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Order;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class OrderService(IOrderRepository orderRepository,IMapper mapper,ITokenService tokenService) : IOrderService
    { 
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITokenService _tokenService = tokenService;




        public virtual async Task<PaginatedResponse<OrderResponseModel>> GetAllOrdersAsync(PaginatedRequest paginated)
        {
           return _mapper.Map<PaginatedResponse<OrderResponseModel>>(await _orderRepository.GetAllAsync(
             paginated, null, o => o.Employee,o=>o.Customer));

           

        }


        public async Task<OrderResponseModel> AddOrderAsync(OrderRequestModel order)
        {   
            var customerID = _tokenService.GetCustomerIDClaim(order.Token);
            var orderEntity = _mapper.Map<Order>(new Order
            {
                CustomerID = customerID,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                OrderDetails = _mapper.Map<ICollection<OrderDetail>>(order.OrderDetails)
            });
            var addedProduct = await _orderRepository.AddAsync(orderEntity);
            var responseDto = _mapper.Map<OrderResponseModel>(addedProduct);

            return responseDto;
        }





        public async Task<OrderResponseModel> GetOrderAsync(int id)
        {
            return _mapper.Map<OrderResponseModel>(await _orderRepository.GetAsync(id));

            
        }

        public async Task<OrderResponseModel> UpdateOrderAsync(OrderUpdateRequestModel order)
        {   var uOrder = _mapper.Map<Order>(order);
            return _mapper.Map<OrderResponseModel>(await _orderRepository.UpdateAsync(uOrder));
        }

        public async Task<int> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<OrderResponseModel>> GetCustomerOrders(string token,PaginatedRequest paginatedRequest)
        {
            var customerID = _tokenService.GetCustomerIDClaim(token);
            return _mapper.Map<PaginatedResponse<OrderResponseModel>>(await _orderRepository.GetAllAsync(
               paginatedRequest,o=>o.CustomerID==customerID , o=>o.Employee,e=>e.Customer));

        }
    }
}
