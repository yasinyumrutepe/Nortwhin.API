﻿namespace Northwind.Core.Models.Request.Auth
{
    public class RegisterRequestModel
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string CustomerID { get; set; }
    }
}
