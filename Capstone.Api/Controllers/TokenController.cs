using AutoMapper;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IStaffService _staffService;
        private readonly IPatronService _patronService;
        private readonly IMapper _mapper;
        public TokenController(IConfiguration configuration, IStaffService staffService, IPatronService patronService, IMapper mapper)
        {
            _configuration = configuration;
            _staffService = staffService;
            _patronService = patronService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Authentication(UserLogin login)
        {
            //if it is a valid user
            var validation = IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }

            return Ok("Wrong username or passsword!!!");
        }

        private (bool, User) IsValidUser(UserLogin login)
        {
            var staff = _staffService.GetLoginByCredenticalsStaff(login);
            var userStaff = _mapper.Map<User>(staff);
            var patron = _patronService.GetLoginByCredenticalsPatron(login);
            var userPatron = _mapper.Map<User>(patron);
            if (userStaff != null )
            {
                return (userStaff != null, userStaff);
            }else if(userPatron != null)
            {
                return (userPatron != null, userPatron);
            }
            return (userStaff != null, userStaff);
        }

        private string GenerateToken(User user)
        {
            //Headers
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            if (user.DeviceToken == null)
            {
                user.DeviceToken = "";
            }
            //Claims
            var claims = new[]
            {              
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("image", user.Image),
                new Claim("userName", user.Username),
                new Claim("address", user.Address),
                new Claim("doB", user.DoB.ToString()),
                new Claim("createdTime", user.CreatedTime.ToString()),
                new Claim("email", user.Email),
                new Claim("password", user.Password),
                new Claim("deviceToken", user.DeviceToken),
                new Claim("gender", user.Gender),
                new Claim("phone", user.Phone),
                new Claim("roleId", user.RoleId.ToString())
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
