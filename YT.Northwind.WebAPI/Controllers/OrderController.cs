using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Order;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : Controller
    {
       private readonly IOrderService _orderService = orderService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedRequest paginatedRequest)
        {
            var orders = await _orderService.GetAllOrdersAsync(paginatedRequest);
            return Ok(orders);
        }


        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerOrders([FromQuery] PaginatedRequest paginatedRequest)
        {
            var token = Request.Headers.Authorization.ToString();
            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
            var orders = await _orderService.GetCustomerOrders(token, paginatedRequest);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]

        public async Task<IActionResult> Add([FromBody] OrderRequestModel orderRequest)
        {

            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString() ?? "127.0.0.1";
            var token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
            }
            var addedOrder = await _orderService.AddOrderAsync(token, ipAddress, orderRequest);
            return CreatedAtAction("Get", new { id = addedOrder.OrderID }, addedOrder);
        }

        [HttpPut("status")]
        public async Task<IActionResult> ChangeOrderStatus([FromBody] ChangeOrderStatusRequestModel changeOrderStatusRequest)
        {
            var updatedOrder = await _orderService.ChangeOrderStatusAsync(changeOrderStatusRequest);
            return Ok(updatedOrder);
        }



        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _orderService.DeleteOrderAsync(id);
           
        }
    }
}
