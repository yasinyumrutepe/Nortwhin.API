

using AutoMapper;
using 
    Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Auth;
using Northwind.Core.Models.Response.Auth;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;
namespace Northwind.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService, IMapper mapper)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<LoginResponseModel> LoginUserAsync(LoginRequestModel request)
        {
            // Kullanıcıyı doğrula
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

        public async Task<User> RegisterUserAsync(RegisterRequestModel request)
        {
            var userEntity = new User
            {   

                Email = request.Email,
                Password = _authRepository.HashPassword(request.Password),
                CustomerID = request.CustomerID
            };
          

            return  await _authRepository.AddAsync(userEntity);
          
        }
    }
}
