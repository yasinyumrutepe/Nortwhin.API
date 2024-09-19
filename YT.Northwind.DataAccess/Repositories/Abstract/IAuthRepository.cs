using Northwind.Core.Models.Request.Auth;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Abstract
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginRequestModel loginRequest);
        Task<User> RegisterAsync(RegisterRequestModel registerRequest);
        byte[] HashPassword(string password);
        bool VerifyPassword(string password, byte[] passwordHash);
    }
}
