using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUriService _uriService;
        public CustomerController(ICustomerService customerService, IConfiguration configuration, IMapper mapper, IUriService uriService)
        {
            _customerService = customerService;
            _mapper = mapper;
            _uriService = uriService;
            _configuration = configuration;
        }
        [HttpGet(Name = nameof(GetCustomers))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CustomerDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCustomers([FromQuery] CustomerQueryFilter filters)
        {
            var books = _customerService.GetCustomers(filters);
            var booksDtos = _mapper.Map<IEnumerable<CustomerDto>>(books);
            var metadata = new Metadata
            {
                TotalCount = books.TotalCount,
                PageSize = books.PageSize,
                CurrentPage = books.CurrentPage,
                TotalPages = books.TotalPages,
                HasNextPage = books.HasNextPage,
                HasPreviousPage = books.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<CustomerDto>>(booksDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _customerService.GetCustomer(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            var response = new ApiResponse<CustomerDto>(customerDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Customer(CustomerDto customerDto)
        {

            var tmp = _customerService.GetCustomer(customerDto.Email);
            if (tmp == null)
            {
                var customer = _mapper.Map<Customer>(customerDto);
                _customerService.InsertCustomer(customer);
                customerDto = _mapper.Map<CustomerDto>(customer);
                var response = new ApiResponse<CustomerDto>(customerDto);
                return Ok(response);
            }
            else
            {
                var token = GenerateToken(customerDto);
                return Ok(new { token });
            }
        }

        [HttpPut]
        public IActionResult Put(int id, CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Id = id;

            var result = _customerService.UpdateCustomer(customer);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        private string GenerateToken(CustomerDto customerDto)
        {
            //Headers
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim("id", customerDto.Id.ToString()),
                new Claim("name", customerDto.Name),
                new Claim("address", customerDto.Address),
                new Claim("DoB", customerDto.DoB.ToString()),
                new Claim("email", customerDto.Email),
                new Claim("gender", customerDto.Gender),
                new Claim("phone", customerDto.Phone)
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

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result =  _customerService.DeleteCustomer(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
