
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Order;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Order;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.DataAccess.Repositories.Concrete;
using Northwind.Entities.Concrete;
using Stripe;

namespace Northwind.Business.Concrete
{
    public class OrderService(IOrderRepository orderRepository,IOrderStatusRepository orderStatusRepository,IMapper mapper,ITokenService tokenService,IBasketService basketService) : IOrderService
    { 
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IBasketService _basketService = basketService;
        private readonly IOrderStatusRepository _orderStatusRepository = orderStatusRepository;




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
          .Include(o => o.OrderStatuses).ThenInclude(o=>o.Status));
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
                ShipAddress = orderRequest.ShipAddress,
                ShipCity = orderRequest.ShipCity,
                ShipCountry = orderRequest.ShipCountry,
                ShipName = orderRequest.ShipName,
                Freight = orderRequest.Freight,
                OrderNumber = orderNumber,
                TotalPrice = basket.TotalPrice,
                OrderDetails = _mapper.Map<ICollection<OrderDetail>>(orderDetails),
                OrderStatuses = [new OrderStatus { StatusID = 1, CreatedAt = DateTime.Now }]
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
            return _mapper.Map<OrderResponseModel>(await _orderRepository.GetAsync(filter:o=>o.OrderID==id));

            
        }

        public async Task<OrderResponseModel> UpdateOrderAsync(OrderUpdateRequestModel order)
        {   var uOrder = _mapper.Map<Order>(order);
            return _mapper.Map<OrderResponseModel>(await _orderRepository.UpdateAsync(uOrder));
        }

        public async Task<int> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetAsync(filter: o => o.OrderID == id);
            return await _orderRepository.DeleteAsync(order);
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
        .Include(o => o.OrderStatuses).ThenInclude(o => o.Status));
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
            
            var orderStatus = new OrderStatus
            {
                OrderID = changeOrderStatusRequestModel.OrderID,
                StatusID = changeOrderStatusRequestModel.StatusID,
                CreatedAt = DateTime.Now
            };
           var isAdded =  await _orderStatusRepository.AddAsync(orderStatus);
            if (isAdded == null)
            {
                return null;
            }
            var order = await _orderRepository.GetAsync(filter: o => o.OrderID == changeOrderStatusRequestModel.OrderID);
            return _mapper.Map<OrderResponseModel>(order);

        }
    }
}
