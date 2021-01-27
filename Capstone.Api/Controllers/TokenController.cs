using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
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
        private readonly IStaffService _staffService;
        public TokenController(IConfiguration configuration, IStaffService staffService)
        {
            _configuration = configuration;
            _staffService = staffService;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            //if it is a valid user
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }

            return Ok("Wrong username or passsword!!!");
        }

        private async Task<(bool, StaffDto)> IsValidUser(UserLogin login)
        {
            var user = await _staffService.GetLoginByCredenticals(login);
            //var isValid = _passwordService.Check(user.Password, login.Password);
            return (user!=null, user);
        }

        private string GenerateToken(StaffDto staffDto)
        {
            //Headers
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim("id", staffDto.Id.ToString()),
                new Claim(ClaimTypes.Name, staffDto.Name),
                new Claim("userName", staffDto.Username),
                new Claim("address", staffDto.Address),
                new Claim("DoB", staffDto.DoB.ToString()),
                new Claim("email", staffDto.Email),
                new Claim("gender", staffDto.Gender),
                new Claim("phone", staffDto.Phone),
                new Claim("role", staffDto.RoleName.ToString())
            };

            //Payloads
            var payload = new JwtPayload
            (
               _configuration["Authentication:Issuer"],
               _configuration["Authentication:Audience"],
               claims,
               DateTime.UtcNow,
               DateTime.UtcNow.AddMinutes(10)

            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
