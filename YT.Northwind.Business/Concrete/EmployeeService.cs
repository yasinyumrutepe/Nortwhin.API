using AutoMapper;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Employee;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Employee;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PaginatedResponse<EmployeeResponseModel>> GetAllEmployeesAsync(PaginatedRequest paginated)
        {
           var employees = _mapper.Map<List<EmployeeResponseModel>>(await _employeeRepository.GetAllAsync(paginated));
           return _mapper.Map<PaginatedResponse<EmployeeResponseModel>>(employees);
        }
        public async Task<EmployeeResponseModel> AddEmployeeAsync(EmployeeRequestModel employee)
        {
            var employeeEntity = _mapper.Map<Employee>(employee);
            var addedEmployee = await _employeeRepository.AddAsync(employeeEntity);
            var responseDto = _mapper.Map<EmployeeResponseModel>(addedEmployee);
            return responseDto;
        }
      

        public async Task<EmployeeResponseModel> GetEmployeeAsync(int id)
        {
            return _mapper.Map<EmployeeResponseModel>(await _employeeRepository.GetAsync(e=>e.EmployeeID == id));
        }

        public async Task<EmployeeResponseModel> UpdateEmployeeAsync(EmployeeUpdateRequestModel employee)
        {   var updatedEmployee = _mapper.Map<Employee>(employee);
            return _mapper.Map<EmployeeResponseModel>(await _employeeRepository.UpdateAsync(updatedEmployee));
        }

        public async Task<int> DeleteEmployeeAsync(int id)
        {
          return  await _employeeRepository.DeleteAsync(id);
        }
    }
}
