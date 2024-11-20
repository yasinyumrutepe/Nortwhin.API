

using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            return _mapper.Map<CustomerResponseModel>(await _customerRepository.GetAsync(c=>c.CustomerID == customerID,include:c=>c.Include(c=>c.User)));
        }
        public async Task<CustomerResponseModel> AddCustomerAsync(CustomerRequestModel customer)
        {
            var productEntity = _mapper.Map<Customer>(customer);
            var addedCustomer = await _customerRepository.AddAsync(productEntity);

            await _customerRepository.GetAsync(c=>c.CustomerID== addedCustomer.CustomerID);
            var responseDto = _mapper.Map<CustomerResponseModel>(addedCustomer);
            return responseDto;
        }

      

       

        public async Task<CustomerResponseModel> GetCustomerAsync(string id)
        {  
           return  _mapper.Map<CustomerResponseModel>(await _customerRepository.GetAsync(filter:c=>c.CustomerID == id));
        }

        public async Task<CustomerResponseModel> UpdateCustomerAsync(CustomerUpdateRequestModel customer)
        {
            var isExistCustomer = await _customerRepository.GetAsync(filter: c => c.CustomerID == customer.CustomerID,include:c => c.Include(c => c.User));
            if (isExistCustomer == null)
            {
                return null;
            }
            isExistCustomer.CustomerID = customer.CustomerID;
            isExistCustomer.ContactName = customer.Name;
            isExistCustomer.Phone = customer.Phone;

            if(isExistCustomer.User.Email != customer.Email)
            {
                isExistCustomer.User.Email = customer.Email;
            }
            var updatedCustomer = _mapper.Map<Customer>(isExistCustomer);
            return  _mapper.Map<CustomerResponseModel>(await _customerRepository.UpdateAsync(updatedCustomer));
        }
        public async Task<int> DeleteCustomerAsync(string id)
        {
            var customer = await _customerRepository.GetAsync(filter: c => c.CustomerID == id);
            return  await _customerRepository.DeleteAsync(customer);
        }

       
    }
}
