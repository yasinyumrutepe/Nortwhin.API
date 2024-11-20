
using System.Security.Cryptography;
using System.Text;
using 
    Northwind.Core.Models.Request.Auth;
using Northwind.Core.Models.Request.User;
using Northwind.Core.Models.Response.Auth;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class AuthRepository(NorthwindContext context) : GenericRepository<User, NorthwindContext>(context), IAuthRepository
    {
        public async Task<User> LoginAsync(LoginRequestModel loginRequest)
        {
            var user = await GetAsync(filter:u => u.Email == loginRequest.Email);
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

        public async Task<User> RegisterAsync(RegisterRepositoryModel registerRequest)
        {
           
            var existingUser = await GetAsync(filter:u => u.Email == registerRequest.Email);
            if (existingUser != null)
            {
                return null;
            }

            var user = new User
            {
                Email = registerRequest.Email,
                UserTypeID = 2,
                Password = HashPassword(registerRequest.Password),
                CustomerID = registerRequest.CustomerID,
            };

          var addUser =   await AddAsync(user);
            if (addUser == null)
            {
                return null; 
            }

            return addUser; 
        }


        public async Task<ChangePasswordResponseModel> UpdatePasswordAsync(ChangePasswordRequestModel changePassword)
        {
            var user = await GetAsync(filter: u => u.CustomerID == changePassword.CustomerID);
            if (user == null)
            {
                return new ChangePasswordResponseModel
                {
                    IsChange = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                };
            }


            bool isPasswordValid = VerifyPassword(changePassword.CurrentPassword, user.Password);

            if (!isPasswordValid) {
                return new ChangePasswordResponseModel
                {
                    IsChange = false,
                    StatusCode = System.Net.HttpStatusCode.NotExtended,
                };
            }


            user.Password = HashPassword(changePassword.NewPassword);
            var updatedPassword = await UpdateAsync(user);

            if (updatedPassword == null)
            {
                return new ChangePasswordResponseModel
                {
                    IsChange = false,
                    StatusCode = System.Net.HttpStatusCode.NotModified,
                };
            }
            return new ChangePasswordResponseModel
            {
                IsChange = true,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
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
