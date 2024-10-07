using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Filter
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
            {
                var token = context.HttpContext.Request.Headers.Authorization.ToString();
                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedResult();
                }

                if (token.StartsWith("Bearer "))
                {
                    token = token["Bearer ".Length..].Trim();
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
            var userTypeID = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
                if (userTypeID == null || userTypeID.Value != "1")
                {
                    context.Result = new UnauthorizedResult();
                }





            }
        }
    }
