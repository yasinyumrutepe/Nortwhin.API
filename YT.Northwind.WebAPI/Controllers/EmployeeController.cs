using Microsoft.AspNetCore.Mvc;
using 
    Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Employee;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedRequest paginatedRequest)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(paginatedRequest);
            return Ok(employees);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]

        public async Task<IActionResult> Add([FromBody] EmployeeRequestModel employee)
        {
            var addedEmployee = await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction("Get", new { id = addedEmployee.EmployeeID }, addedEmployee);
        }

        [HttpPut]

        public async Task<IActionResult> Update([FromBody] EmployeeUpdateRequestModel employee)
        {
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employee);
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _employeeService.DeleteEmployeeAsync(id);
           
        }


    }
}
