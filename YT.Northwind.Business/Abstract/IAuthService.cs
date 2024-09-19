using Northwind.Core.Models.Request.Auth;
using Northwind.Core.Models.Response.Auth;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Abstract
{
    public interface IAuthService
    {
        Task<LoginResponseModel> LoginUserAsync(LoginRequestModel request);
        Task<User> RegisterUserAsync(RegisterRequestModel request);


    }
}
