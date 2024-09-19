using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Customer;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : Controller
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedRequest paginatedRequest)
        {
            var customers = await _customerService.GetAllCustomersAsync(paginatedRequest);
            return Ok(customers);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);

        }
        [HttpGet("token/{token}")]
        public async Task<IActionResult> GetCustomer(string token)
        {
            var customer = await _customerService.GetCustomerByTokenAsync(token);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] CustomerRequestModel customer)
        {
            var addedCustomer = await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = addedCustomer.CustomerID }, addedCustomer);
        }

        [HttpPut]

        public async Task<IActionResult> Update([FromBody] CustomerUpdateRequestModel customer)
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(customer);
            return Ok(updatedCustomer);
        }
        [HttpDelete]

        public async Task<int> Delete(int id)
        {
            return await _customerService.DeleteCustomerAsync(id);
            
        }


    }
}
