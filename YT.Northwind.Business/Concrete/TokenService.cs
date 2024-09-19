

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Northwind.Business.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class TokenService : ITokenService
    {
        public string GenerateJWTToken(User user)
        {   
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your-secure-key-must-be-at-least-32-characters-long");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new(ClaimTypes.Name, "USER"),
                     new(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                     new(ClaimTypes.Email, user.Email),
                     new(JwtRegisteredClaimNames.Sub,user.CustomerID)

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetCustomerIDClaim(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var customerIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

            return customerIdClaim?.Value;
        }
    }
}
