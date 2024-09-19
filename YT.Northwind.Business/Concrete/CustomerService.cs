

using AutoMapper;
using 
    Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Customer;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Customer;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class CustomerService(ICustomerRepository customerRepository, IMapper mapper,ITokenService tokenService) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<PaginatedResponse<CustomerResponseModel>> GetAllCustomersAsync(PaginatedRequest paginated)
        {   
            

            return _mapper.Map<PaginatedResponse<CustomerResponseModel>>(await _customerRepository.GetAllAsync(paginated) );
           
        }

        public async Task<CustomerResponseModel> GetCustomerByTokenAsync(string token)
        {   var customerID = _tokenService.GetCustomerIDClaim(token);
            return _mapper.Map<CustomerResponseModel>(await _customerRepository.GetAsync(c=>c.CustomerID == customerID,c=>c.User));
        }
        public async Task<CustomerResponseModel> AddCustomerAsync(CustomerRequestModel customer)
        {
            var productEntity = _mapper.Map<Customer>(customer);
            var addedCustomer = await _customerRepository.AddAsync(productEntity);

            await _customerRepository.GetAsync(c=>c.CustomerID== addedCustomer.CustomerID);
            var responseDto = _mapper.Map<CustomerResponseModel>(addedCustomer);
            return responseDto;
        }

      

       

        public async Task<CustomerResponseModel> GetCustomerAsync(int id)
        {  
           return  _mapper.Map<CustomerResponseModel>(await _customerRepository.GetAsync(id));
        }

        public async Task<CustomerResponseModel> UpdateCustomerAsync(CustomerUpdateRequestModel customer)
        {  var updatedCustomer = _mapper.Map<Customer>(customer);
            return  _mapper.Map<CustomerResponseModel>(await _customerRepository.UpdateAsync(updatedCustomer));
        }
        public async Task<int> DeleteCustomerAsync(int id)
        {
          return  await _customerRepository.DeleteAsync(id);
        }

       
    }
}
