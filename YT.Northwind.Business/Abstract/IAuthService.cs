using Northwind.Core.Models.Request.Auth;
using Northwind.Core.Models.Request.User;
using Northwind.Core.Models.Response.Auth;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Abstract
{
    public interface IAuthService
    {
        Task<LoginResponseModel> LoginUserAsync(LoginRequestModel request);
        Task<LoginResponseModel> RegisterUserAsync(RegisterRequestModel request);
        Task<ChangePasswordResponseModel> ChangePasswordAsync(ChangePasswordRequestModel request);

    }
}
