using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Middleware
{
    public class UserTypeCheckMiddleware
    {
        private readonly RequestDelegate _next;
        public UserTypeCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userClaims = context.User.Claims;
            var userTypeClaim = userClaims.FirstOrDefault(c => c.Type == "UserTypeID");

            Console.Write(userTypeClaim.Value);


            if (userTypeClaim != null && userTypeClaim.Value != "1")
            {
                context.Response.Redirect("/access-denied");
                return;
            }
            await _next(context);
        }
    }
}
