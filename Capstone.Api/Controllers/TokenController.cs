using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Capstone.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authentication(UserLogin login)
        {
            if (IsValidUser(login))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return NotFound();
        }

        private bool IsValidUser(UserLogin login)
        {
            return true;
        }

        private string GenerateToken()
        {
            //Headers
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Jonathan Estrada"),
                new Claim(ClaimTypes.Email, "huulinhnguyen99@gmail.com"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            //Payloads
            var payload = new JwtPayload
            (
               _configuration["Authentication:Issuer"],
               _configuration["Authentication:Audience"],
               claims,
               DateTime.Now,
               DateTime.UtcNow.AddMinutes(2)

            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
