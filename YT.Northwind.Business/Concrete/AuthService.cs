

using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using 
    Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Auth;
using Northwind.Core.Models.Request.User;
using Northwind.Core.Models.Response.Auth;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.DataAccess.Repositories.Concrete;
using Northwind.Entities.Concrete;
using System.Security.Cryptography;
using System.Text;
namespace Northwind.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public AuthService(IAuthRepository authRepository,ICustomerRepository customerRepository, ITokenService tokenService, IMapper mapper)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<LoginResponseModel> LoginUserAsync(LoginRequestModel request)
        {
            var user = await _authRepository.LoginAsync(request);
            if (user == null)
            {
               return new LoginResponseModel
               {
                   Token = null,
                   Message = "Kullanıcı adı veya şifre hatalı",
                   Status = 400

               };   
            }


            var token = _tokenService.GenerateJWTToken(_mapper.Map<User>(user));

            return new LoginResponseModel
            {
                Token = token,
                Message = "Login Başarılı",
                Status = 200
            };
        }

        public async Task<LoginResponseModel> RegisterUserAsync(RegisterRequestModel request)
        {

            if(request.Password != request.ConfirmPassword)
            {
                return null;
            }

            var isUser = await _authRepository.GetAsync(filter: u => u.Email == request.Email);

            if (isUser != null) {
                return null;
            }

            Customer customer = new Customer
            {
                CustomerID = GenerateUniqueId(request.FirstName),
                ContactName = request.FirstName + " " + request.LastName,
                CompanyName = request.FirstName + " " + request.LastName,
                ContactTitle = request.FirstName + " " + request.LastName,
            };

            var newCustomer = await _customerRepository.AddAsync(customer);
            if (newCustomer == null)
            {
                return null;
            }

            RegisterRepositoryModel registerRepositoryModel = new RegisterRepositoryModel();
            registerRepositoryModel.Email = request.Email;
            registerRepositoryModel.Password = request.Password;
            registerRepositoryModel.CustomerID = newCustomer.CustomerID;


            var newUser = await _authRepository.RegisterAsync(registerRepositoryModel);

            if (newUser == null)
            {
                return null;
            }

            var token = _tokenService.GenerateJWTToken(newUser);

            return new LoginResponseModel
            {
                Token = token,
                Message = "Login Başarılı",
                Status = 200
            };
        }
        
        public async Task<ChangePasswordResponseModel> ChangePasswordAsync(ChangePasswordRequestModel request)
        {

                return await _authRepository.UpdatePasswordAsync(request);
        }

        public static string GenerateUniqueId(string name)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(name));

                var base64 = Convert.ToBase64String(hashBytes);
                return base64.Substring(0, 5);
            }
        }
    }
}
