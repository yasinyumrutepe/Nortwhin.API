
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Order;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Order;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;
using Stripe;

namespace Northwind.Business.Concrete
{
    public class OrderService(IOrderRepository orderRepository,IMapper mapper,ITokenService tokenService,IBasketService basketService) : IOrderService
    { 
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IBasketService _basketService = basketService;




        public virtual async Task<PaginatedResponse<OrderResponseModel>> GetAllOrdersAsync(PaginatedRequest paginated)
        {
            var orders = await _orderRepository.GetAllAsync2(paginated, 
                  include: i => i
          .Include(o => o.OrderDetails)
              .ThenInclude(od => od.Product)
                  .ThenInclude(p => p.ProductImages)
          .Include(o => o.OrderDetails)
              .ThenInclude(od => od.Product)
                  .ThenInclude(p => p.ProductReviews)
          .Include(o => o.OrderStatus));
            return _mapper.Map<PaginatedResponse<OrderResponseModel>>(orders);



        }


        public async Task<OrderResponseModel> AddOrderAsync(string token,OrderRequestModel orderRequest)
        {       
            var customerID = _tokenService.GetCustomerIDClaim(token);
            var basket = _basketService.GetBasket(token);
            var orderDetails = new List<OrderDetailRequestModel>();
            var orderNumber = await GenerateOrderNumber();
            foreach (var item in basket.Items) {
                var orderDetail = new OrderDetailRequestModel
                {
                    ProductID = item.ProductID,
                    Quantity = short.Parse(item.Quantity.ToString()),
                    UnitPrice = item.UnitPrice,
                    Discount = false
                };
                orderDetails.Add(orderDetail);
            }
            var orderEntity = _mapper.Map<Order>(new Order
            {
                CustomerID = customerID,
                OrderDate = orderRequest.OrderDate,
                RequiredDate = orderRequest.RequiredDate,
                ShippedDate = orderRequest.ShippedDate,
                ShipAddress = orderRequest.ShipAddress,
                ShipCity = orderRequest.ShipCity,
                ShipCountry = orderRequest.ShipCountry,
                ShipName = orderRequest.ShipName,
                Freight = orderRequest.Freight,
                OrderStatusID = 1,
                OrderNumber = orderNumber,
                TotalPrice = basket.TotalPrice,
                OrderDetails = _mapper.Map<ICollection<OrderDetail>>(orderDetails)
            });
            var addedProduct = await _orderRepository.AddAsync(orderEntity);

            if (addedProduct == null)
            {
                return null;
            }

            _basketService.ClearBasket(token);
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
            var orders = await _orderRepository.GetAllAsync2(paginatedRequest,predicate:c=>c.CustomerID ==customerID,
                include: i => i
        .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
                .ThenInclude(p => p.ProductImages)
        .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
                .ThenInclude(p => p.ProductReviews.Where(pr=>pr.CustomerID==customerID))
        .Include(o => o.OrderStatus));
            return _mapper.Map<PaginatedResponse<OrderResponseModel>>(orders);

        }


        public async  Task<string> GenerateOrderNumber()
        {
            Random random = new();
            int firstGroup = random.Next(100, 1000); 
            int secondGroup = random.Next(100, 1000);
            int thirdGroup = random.Next(100, 1000);
            string orderNumber = $"{firstGroup}-{secondGroup}-{thirdGroup}";
            var isOrder =  await _orderRepository.GetAsync(o=>o.OrderNumber == orderNumber);
            if (isOrder != null)
            {
                return await GenerateOrderNumber();
            }
            return orderNumber;
        }

        public async Task<OrderResponseModel> ChangeOrderStatusAsync(ChangeOrderStatusRequestModel changeOrderStatusRequestModel)
        {
            var order = await _orderRepository.GetAsync(changeOrderStatusRequestModel.OrderID);
            if (order == null)
            {
                return null;
            }
            order.OrderStatusID = changeOrderStatusRequestModel.OrderStatusID;
            var updatedOrder = await _orderRepository.UpdateAsync(order);
            return _mapper.Map<OrderResponseModel>(updatedOrder);
        }
    }
}
