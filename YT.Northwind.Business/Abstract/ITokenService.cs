
using  Northwind.Entities.Concrete;

namespace Northwind.Business.Abstract
{
    public interface ITokenService
    {
        public string GenerateJWTToken(User user);

        public string GetCustomerIDClaim(string token);

    }
}
