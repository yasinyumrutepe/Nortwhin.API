using Northwind.Core.Models.Request.Auth;
using Northwind.Core.Models.Request.User;
using Northwind.Core.Models.Response.Auth;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Abstract
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginRequestModel loginRequest);
        Task<User> RegisterAsync(RegisterRepositoryModel registerRequest);
        Task<ChangePasswordResponseModel> UpdatePasswordAsync(ChangePasswordRequestModel changePasswordRequest);
        byte[] HashPassword(string password);
        bool VerifyPassword(string password, byte[] passwordHash);
    }
}
