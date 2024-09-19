
using System.Security.Cryptography;
using System.Text;
using 
    Northwind.Core.Models.Request.Auth;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class AuthRepository(NorthwindContext context) : GenericRepository<User, NorthwindContext>(context), IAuthRepository
    {
        public async Task<User> LoginAsync(LoginRequestModel loginRequest)
        {
            var user = await GetAsync(u => u.Email == loginRequest.Email);
            if (user == null)
            {
                return null; 
            }

            bool isPasswordValid = VerifyPassword(loginRequest.Password, user.Password);

            if (!isPasswordValid)
            {
                return null; 
            }

            return user; 
        }

        public async Task<User> RegisterAsync(RegisterRequestModel registerRequest)
        {
           
            var user = new User
            {
                Email = registerRequest.Email,
                Password = HashPassword(registerRequest.Password),
            };

          var addUser =   await AddAsync(user);
            if (addUser == null)
            {
                return null; 
            }

            return addUser; 
        }

        public byte[] HashPassword(string password)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPassword(string inputPassword, byte[] storedHash)
        {
            var inputHash = HashPassword(inputPassword);
            return inputHash.SequenceEqual(storedHash);
        }
    }

}
