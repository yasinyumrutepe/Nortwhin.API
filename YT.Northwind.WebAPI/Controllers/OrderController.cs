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


        [HttpGet("customer/{token}")]
        public async Task<IActionResult> GetCustomerOrders(string token, [FromQuery] PaginatedRequest paginatedRequest)
        {
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

        public async Task<IActionResult> Add([FromBody] OrderRequestModel order)
        {
            var addedOrder = await _orderService.AddOrderAsync(order);
            return CreatedAtAction("Get", new { id = addedOrder.OrderID }, addedOrder);
        }

        [HttpPut]

        public async Task<IActionResult> Update([FromBody] OrderUpdateRequestModel order)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(order);
            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _orderService.DeleteOrderAsync(id);
           
        }
    }
}
