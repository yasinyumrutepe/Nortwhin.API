
using System.Net;


namespace Northwind.Core.Models.Response.Auth
{
    public class ChangePasswordResponseModel
    {
        public bool IsChange { get; set; }
        public HttpStatusCode  StatusCode { get; set; }
    }
}
