﻿using Microsoft.AspNetCore.Mvc;
using 
    Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Auth;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestModel loginRequest)
        {
            
            var loginResponse = await _authService.LoginUserAsync(loginRequest);
            return Ok(loginResponse);

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerRequest)
        {
           var registerResponse = await _authService.RegisterUserAsync(registerRequest);
            return Ok(registerResponse);
        }
    }
}